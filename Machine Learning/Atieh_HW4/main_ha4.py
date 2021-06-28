from __future__ import absolute_import
from __future__ import division
from __future__ import print_function

import numpy as np
import scipy
import matplotlib.pyplot as plt
from keras.datasets import mnist
from util import func_confusion_matrix
import tensorflow as tf


import time
from datetime import datetime
import os.path
import data_helpers
import func_two_layer_fc
import sys


# load (downloaded if needed) the MNIST dataset
(x_train, y_train), (x_test, y_test) = mnist.load_data()


# transform each image from 28 by28 to a 784 pixel vector
pixel_count = x_train.shape[1] * x_train.shape[2]
x_train = x_train.reshape(x_train.shape[0], pixel_count).astype('float32')
x_test = x_test.reshape(x_test.shape[0], pixel_count).astype('float32')

# normalize inputs from gray scale of 0-255 to values between 0-1
x_train = x_train / 255
x_test = x_test / 255

# Please write your own codes in responses to the homework assignment 4
#Question 1 : Split training images ( and labels) into two subsets: 50000 images and 10000 images



nTrain=50000
indices = np.random.permutation(x_train.shape[0])


training_idx, validation_idx = indices[:nTrain], indices[nTrain:]

trainX = x_train[training_idx]  #  training samples
trainY = y_train[training_idx] # labels of training samples

validationX = x_train[validation_idx] # validation samples
validationY = y_train[validation_idx]  # labels of validation samples

####################################################################
############## step-0: setting parameters       ####################
####################################################################

# Model parameters as external flags
flags = tf.flags
FLAGS = flags.FLAGS
flags.DEFINE_float('learning_rate', 0.05, 'Learning rate for the training.')
flags.DEFINE_integer('max_steps', 2000, 'Number of steps to run trainer.')
flags.DEFINE_integer('hidden1', 400, 'Number of units in hidden layer 1.')
flags.DEFINE_integer('hidden2', 100, 'Number of units in hidden layer 2.')
flags.DEFINE_integer('batch_size', 400,
  'Batch size. Must divide dataset sizes without remainder.')
flags.DEFINE_string('train_dir', 'tf_logs',
  'Directory to put the training data.')
flags.DEFINE_float('reg_constant', 0.1, 'Regularization constant.')

#FLAGS._parse_flags()
FLAGS(sys.argv)
print('\nParameters:')
for attr, value in sorted(FLAGS.__flags.items()):
  print('{} = {}'.format(attr, value))
print()

logdir = FLAGS.train_dir + '/' + datetime.now().strftime('%Y%m%d-%H%M%S') + '/'

IMAGE_PIXELS = 784
CLASSES = 10

beginTime = time.time()

####################################################################
############## step-2: Prepare the Tensorflow graph ################
####################################################################

# -----------------------------------------------------------------------------
# Prepare the Tensorflow graph
# (We're only defining the graph here, no actual calculations taking place)
# -----------------------------------------------------------------------------

# Define input placeholders
images_placeholder = tf.placeholder(tf.float32, shape=[None, IMAGE_PIXELS],
  name='images')
labels_placeholder = tf.placeholder(tf.int64, shape=[None], name='image-labels')

# Operation for the classifier's result
logits = func_two_layer_fc.inference(images_placeholder, IMAGE_PIXELS,
  FLAGS.hidden1,FLAGS.hidden2, CLASSES, reg_constant=FLAGS.reg_constant)

# logits = func_two_layer_fc.inference(images_placeholder, IMAGE_PIXELS,
#   FLAGS.hidden1, CLASSES, reg_constant=FLAGS.reg_constant)


# Operation for the loss function
loss = func_two_layer_fc.loss(logits, labels_placeholder)

# Operation for the training step
train_step = func_two_layer_fc.training(loss, FLAGS.learning_rate)

# Operation calculating the accuracy of our predictions
accuracy = func_two_layer_fc.evaluation(logits, labels_placeholder)


# Operation merging summary data for TensorBoard
summary = tf.summary.merge_all()

# Define saver to save model state at checkpoints
saver = tf.train.Saver()


# -----------------------------------------------------------------------------
# Run the TensorFlow graph
# -----------------------------------------------------------------------------

with tf.Session() as sess:
  # Initialize variables and create summary-writer
  sess.run(tf.global_variables_initializer())
  summary_writer = tf.summary.FileWriter(logdir, sess.graph)

  # Generate input data batches
  zipped_data = zip(trainX,trainY)
  batches = data_helpers.gen_batch(list(zipped_data), FLAGS.batch_size,
    FLAGS.max_steps)

  for i in range(FLAGS.max_steps):

    # Get next input data batch
    batch = next(batches)
    images_batch, labels_batch = zip(*batch)
    feed_dict = {
      images_placeholder: images_batch,
      labels_placeholder: labels_batch
    }

    # Periodically print out the model's current accuracy
    if i % 100 == 0:
      train_accuracy = sess.run(accuracy, feed_dict=feed_dict)
      print('Step {:d}, training accuracy {:g}'.format(i, train_accuracy))
      summary_str = sess.run(summary, feed_dict=feed_dict)
      summary_writer.add_summary(summary_str, i)

    # Perform a single training step
    sess.run([train_step, loss], feed_dict=feed_dict)

    # Periodically save checkpoint
    if (i + 1) % 1000 == 0:
      checkpoint_file = os.path.join(FLAGS.train_dir, 'checkpoint')
      saver.save(sess, checkpoint_file, global_step=i)
      print('Saved checkpoint')


  # After finishing the training, evaluate on the validation set
  # test_accuracy = sess.run(accuracy, feed_dict={
  #   images_placeholder: validationX,
  #   labels_placeholder: validationY})
  # print('validation accuracy {:g}'.format(test_accuracy))


  # After finishing the training, evaluate on the test set
  test_accuracy = sess.run(accuracy, feed_dict={
    images_placeholder: x_test,
    labels_placeholder: y_test})
  print('Test accuracy {:g}'.format(test_accuracy))

  prediction = sess.run(tf.argmax(logits,1), feed_dict={
    images_placeholder: validationX})

endTime = time.time()
print('Total time: {:5.2f}s'.format(endTime - beginTime))



conf_matrix, accuracy, recall_array, precision_array=func_confusion_matrix(validationY,prediction)
print("confusion_matrix")
print(conf_matrix)
print("accuracy")
print(accuracy)
print("recall_array")
print(recall_array)
print("precision_array")
print(precision_array)


# -----------------------------------------------------------------------------
#  Plot the testing images for which the FNN model made errors
# -----------------------------------------------------------------------------

def plot_misclassified_testing_image(images, true_labels, predict, title=None):
    """
    Plot the testing images that have been misclassified
    """

    # identify the boolean array that true and predicted label not equal
    misclassified = np.logical_not(np.equal(predict, true_labels))

    # images from the test set that classified incorrectly
    misclassified_images = images[misclassified]

    # For misclassified images get the true and predicted classes
    predicted_images = predict[misclassified]
    true_images = true_labels[misclassified]

    # Plot ten testing misclassified images
    plot_images(images=misclassified_images[0:10],true_class=true_images[0:10],predicted_class=predicted_images[0:10],title=title)


def plot_images(images, true_class, predicted_class,title=None):
    """
    Create figure
    """
    fig, axes = plt.subplots(5, 2, figsize=(10, 10))
    fig.subplots_adjust(hspace=0.3, wspace=0.3)
    for i, ax in enumerate(axes.flat):
        # Plot image
        ax.imshow(images[i].reshape(28, 28), cmap='binary')

        ax.set_xticks([])
        ax.set_yticks([])

        # Show true and predicted classes
        ax_title = "True: {0}, Predicted : {1}".format(true_class[i], predicted_class[i])
        ax.set_title(ax_title)

    plt.suptitle(title, size=20)

    plt.show(block=False)


plot_misclassified_testing_image(x_test, y_test, prediction, title='Misclassified Testing Images')
plt.show()