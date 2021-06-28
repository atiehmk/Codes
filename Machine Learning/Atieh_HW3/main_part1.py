import numpy as np
import matplotlib.pyplot as plt
from getDataset import getDataSet
from sklearn.linear_model import LogisticRegression
import math
import pandas as pd

from pandas import DataFrame
from sklearn import preprocessing
from sklearn.linear_model import LogisticRegression
#from sklearn.cross_validation import train_test_split
from numpy import loadtxt, where
from pylab import scatter, show, legend, xlabel, ylabel
from util import Cost_Function, Gradient_Descent, Cost_Function_Derivative, Cost_Function, Prediction, Sigmoid
#from confusionMatrix import func_calConfusionMatrix
from sklearn.metrics import accuracy_score, confusion_matrix, recall_score, precision_score,classification_report
import pylab as pl

# Starting codes

# Fill in the codes between "%PLACEHOLDER#start" and "PLACEHOLDER#end"

# step 1: generate dataset that includes both positive and negative samples,
# where each sample is described with two features.
# 250 samples in total.

[X, y] = getDataSet()  # note that y contains only 1s and 0s,

# create figure for all charts to be placed on so can be viewed together
fig = plt.figure()


def func_DisplayData(dataSamplesX, dataSamplesY, chartNum, titleMessage):
    idx1 = (dataSamplesY == 0).nonzero()  # object indices for the 1st class
    idx2 = (dataSamplesY == 1).nonzero()
    ax = fig.add_subplot(1, 3, chartNum)
    # no more variables are needed
    plt.plot(dataSamplesX[idx1, 0], dataSamplesX[idx1, 1], 'r*')
    plt.plot(dataSamplesX[idx2, 0], dataSamplesX[idx2, 1], 'b*')
    # axis tight
    ax.set_xlabel('x_1')
    ax.set_ylabel('x_2')
    ax.set_title(titleMessage)


# plotting all samples
func_DisplayData(X, y, 1, 'All samples')

# number of training samples
nTrain = 120

########################### Confusion Matrix ######################
# Implement function to calculate the confusion matrix for the prediction results

def func_calConfusionMatrix(predY,trueY):

    #confusion matrix result
    confusionMatrix = [[0, 0], [0, 0]]

    for i in range(len(predY)):
        if int(trueY[i]) == 0:
            if float(predY[i]) == 0:
                confusionMatrix[0][0] = confusionMatrix[0][0] + 1
            else:
                confusionMatrix[0][1] = confusionMatrix[0][1] + 1
        elif int(trueY[i]) == 1:
            if float(predY[i]) == 0:
                confusionMatrix[1][0] = confusionMatrix[1][0] + 1
            else:
                confusionMatrix[1][1] = confusionMatrix[1][1] + 1

    #Accuracy
    accuracy = float(confusionMatrix[0][0] + confusionMatrix[1][1]) / (len(trueY))

    #Per Class Precision rate
    class_precision_zero = float(confusionMatrix[0][0]/(confusionMatrix[0][0] + confusionMatrix[1][0]))
    class_precision_one = float(confusionMatrix[1][1]/(confusionMatrix[0][1] + confusionMatrix[1][1]))

    # Per Class Recall rate
    class_recall_zero = float(confusionMatrix[0][0]/(confusionMatrix[0][0] + confusionMatrix[0][1]))
    class_recall_one = float(confusionMatrix[1][1]/(confusionMatrix[1][0] + confusionMatrix[1][1]))

    print(confusionMatrix)
    print('Accuracy:',accuracy)
    print('precision_class_1:',class_precision_one)
    print('precision_class_0:',class_precision_zero)
    print('recall_class_1:',class_recall_one)
    print('recall_class_0:',class_recall_zero)


######################PLACEHOLDER 1#start#########################
# write you own code to randomly pick up nTrain number of samples for training and use the rest for testing.
# WARNIN: 

#A random permutation, to split the data randomly
indices = np.random.permutation(X.shape[0])
training_idx, test_idx = indices[:nTrain], indices[nTrain:]

trainX = X[training_idx,:]  #  training samples

trainY = y[training_idx,:] # labels of training samples    nTrain X 1

testX = X[test_idx,:] # testing samples

testY = y[test_idx,:]  # labels of testing samples     nTest X 1



# ####################PLACEHOLDER 1#end#########################
#
# # plot the samples you have pickup for training, check to confirm that both negative
# and positive samples are included.
func_DisplayData(trainX, trainY, 2, 'training samples')
func_DisplayData(testX, testY, 3, 'testing samples')

# show all charts
plt.show()
#
#
# #  step 2: train logistic regression models
#
#
# ######################PLACEHOLDER2 #start#########################
# # in this placefolder you will need to train a logistic model using the training data: trainX, and trainY.


# ########################################################################
# #################Step-3: training and testing using sklearn    #########
# ########################################################################
#
# use sklearn class
clf = LogisticRegression()
# call the function fit() to train the class instance
clf.fit(trainX,trainY)
# scores over testing samples
print("score over testing samples")
print(clf.score(testX,testY))


#######################################################################
##############Step-4: training and testing using self-developed model ##
########################################################################
#
theta = [0,0] #initial model parameters
alpha = 0.1 # learning rates
max_iteration = 1000 # maximal iterations


m = len(trainY) # number of samples
for x in range(max_iteration):
	# call the functions for gradient descent method
	new_theta = Gradient_Descent(trainX,trainY,theta,m,alpha)
	theta = new_theta
	if x % 200 == 0:
		# calculate the cost function with the present theta
		Cost_Function(trainX,trainY,theta,m)
		print ('theta ', theta)
		print ('cost is ', Cost_Function(trainX,trainY,theta,m))

    ########################################################################
    #################         Step-5: comparing two models         #########
    ########################################################################
    ##comparing accuracies of two models.

score = 0
winner = ""
# accuracy for sklearn
scikit_score = clf.score(testX, testY)
length = len(testX)
for i in range(length):
    prediction = round(Prediction(testX[i], theta))
    answer = testY[i]
    if prediction == answer:
        score += 1

my_score = float(score) / float(length)
if my_score > scikit_score:
    print('You won!')
elif my_score == scikit_score:
    print('Its a tie!')
else:
    print('Scikit won.. :(')
print('Your score: ', my_score)
print('Scikits score: ', scikit_score)



# ######################PLACEHOLDER2 #end #########################
#
#
#
# # step 3: Use the model to get class labels of testing samples.
#
#
# ######################PLACEHOLDER3 #start#########################
# codes for making prediction,
# with the learned model, apply the logistic model over testing samples
# WARNING: Write your own codes for making predictions
#  Apply the learned model to get the binary classification of testing samples
# Create Array ProbY for the predicted Y values
# Transform the output using the sigmoid function to return a probability value between 0 and 1
#Getting binary class of testing samples using the self-developed implemenation
# to get binary class of testing sample we need a threshold

probY = np.empty(130)
def predict(theta, x):
    z = 0
    for i in range(len(theta)):
       z += x[i]*theta[i]
    g = float(1.0 / float((1.0 + math.exp(-1.0 * z))))
    return 1 if g >= 0.5 else 0

length = len(testX)
for i in range(length):
    probY[i] = predict(theta,testX[i])


# Getting binary class of testing samples using Sklearn
predictY=clf.predict(testX)


print("The ( predicted labels ) binary classes of testing samples _ from the self-developed model:")
print(probY)
print("The ( predicted labels ) binary classes of testing samples _ from sklearn:")
print(predictY)

# #PLACEHOLDER#end
#
# ######################PLACEHOLDER 3 #end #########################

# step 4: evaluation
# compare predictions labels from self-developed method and true labels testy to calculate average error and standard deviation
testYDiff = np.abs(probY - testY)
avgErr = np.mean(testYDiff)
stdErr = np.std(testYDiff)
print("testYDiff")
print(testYDiff)
print('average error _ self-developed function: {} ({})'.format(avgErr, stdErr))


# step 4: evaluation
# compare predictions labels from sklearn method and true labels testy to calculate average error and standard deviation
testYDiff = np.abs(predictY - testY)
avgErr = np.mean(testYDiff)
stdErr = np.std(testYDiff)
print("testYDiff")
print(testYDiff)
print('average error _ Sklearn function: {} ({})'.format(avgErr, stdErr))


# ######################Comparative Studies #########################
# Use func_calConfusionMatrix for self-developed model of Logistic Regression
print("Calculate confusion matrix for self-developed model of Logistic Regression ")
func_calConfusionMatrix(probY,testY)

# Use func_calConfusionMatrix for Sklearn model of Logistic Regression
print("Calculate confusion matrix for Sklearn model of Logistic Regression")
func_calConfusionMatrix(predictY,testY)








