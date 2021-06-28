import numpy as np
import download_data as dl
import matplotlib.pyplot as plt
import sklearn.svm as svm
from sklearn import metrics
from conf_matrix import func_confusion_matrix
import numpy
import pickle
from confusionMatrix import func_calConfusionMatrix

### Step 1 and Step 2 are in the file dataStore.py

#CS 596, machine learning
## Step 1 and step 2  are implemented in the dataStore.py file. That file needs to run first in order to store the data
## step 1: load data from csv file. 
# data = dl.download_data('crab.csv').values
#
# n = 200
# #split data
# S = np.random.permutation(n)
# #100 training samples
# Xtr = data[S[:100], :6]
# Ytr = data[S[:100], 6:]
# # 100 testing samples
# X_test = data[S[100:], :6]
# Y_test = data[S[100:], 6:].ravel()
#
# ## step 2 randomly split Xtr/Ytr into two even subsets: use one for training, another for validation.
# #############placeholder: training/validation #######################
# n2 = len(Xtr)
# S2 = np.random.permutation(n2)
#
# # subsets for training models
# x_train=  Xtr[S2[:50]]
# y_train= Ytr[S2[:50]]
# # subsets for validation
# x_validation= Xtr[S2[50:]]
# y_validation= Ytr[S2[50:]]
#
# Loading the training ,validation,testing data from the files  that have been stored
x_train = pickle.load(open("xTrain.dat", "rb"))
y_train = pickle.load(open("yTrain.dat", "rb"))


x_validation = pickle.load(open("xValidation.dat", "rb"))
y_validation = pickle.load(open("yValidation.dat", "rb"))


X_test = pickle.load(open("xTest.dat", "rb"))
Y_test = pickle.load(open("yTest.dat", "rb"))



#############placeholder #######################

## step 3 Model selection over validation set
# consider the parameters C, kernel types (linear, RBF etc.) and kernal
# parameters if applicable. 


# 3.1 Plot the validation errors while using different values of C ( with other hyperparameters fixed) 
#  keeping kernel = "linear"
#############placeholder: Figure 1#######################
#
c_range = np.array(range(1, 10, 2))
svm_c_error = []
for c_value in c_range:
    model = svm.SVC(kernel='linear', C=c_value)
    model.fit(X=x_train, y=y_train)
    error = 1. - model.score(x_validation, y_validation)
    svm_c_error.append(error)
    print('C value: {} , Validation error {} \n'.format(c_value, error))
plt.plot(c_range, svm_c_error)
plt.title('Linear SVM')
plt.xlabel('c values')
plt.ylabel('error')
plt.xticks(c_range)
plt.show()


#############placeholder #######################


# 3.2 Plot the validation errors while using linear, RBF kernel, or Polynomial kernel ( with other hyperparameters fixed) 
#############placeholder: Figure 2#######################
kernel_types = ['linear', 'poly', 'rbf']
svm_kernel_error = []
for kernel_value in kernel_types:
    # your own codes
    model = svm.SVC(kernel=kernel_value, C=1)
    model.fit(X=x_train, y=y_train)
    error = 1. - model.score(x_validation, y_validation)
    svm_kernel_error.append(error)
    print('Kernel type: {} , Validation error {} \n'.format(kernel_value,error))

plt.plot(kernel_types, svm_kernel_error)
plt.title('SVM by Kernels')
plt.xlabel('Kernel')
plt.ylabel('error')
plt.xticks(kernel_types)
plt.show()
#
#
# ## step 4 Select the best model and apply it over the testing subset
best_kernel = 'linear'
best_c = 1
model = svm.SVC(kernel=best_kernel, C=best_c)
model.fit(X=x_train, y=y_train)
y_pred = model.predict(X_test)

# ## step 5 evaluate your results with the metrics you have developed in HA3,including accuracy, quantize your results.
# Provided code

conf_matrix, accuracy, recall_array, precision_array = func_confusion_matrix(Y_test, y_pred)

print("Provided code")
print("Confusion Matrix: ")
print(conf_matrix)
print("Average Accuracy: {}".format(accuracy))
print("Per-Class Precision: {}".format(precision_array))
print("Per-Class Recall: {}".format(recall_array))

# My code from HW3
print("My code from HW3")
func_calConfusionMatrix(y_pred,Y_test)

## Visualize the success examples and failure examples
def visualize_failure_result(xTest, true_label,predict):
    misclassified = np.logical_not(np.equal(predict, true_label))
    misclassified_crabs=xTest[misclassified]
    misclassified_crabs=misclassified_crabs[0:5]
    predicted_label = predict[misclassified]
    actual_label = true_label[misclassified]
    print("Failure Examples")
    print("Test samples: {} \n Actual Label: {} \n Predicted Label : {}\n".format(misclassified_crabs, actual_label[0:5],predicted_label[0:5]))

def visualize_success_result(xTest, true_label,predict):
    correct_classified = (np.equal(predict, true_label))
    correct_classified_crabs=xTest[correct_classified]
    predicted_label =predict[correct_classified]
    actual_label = true_label[correct_classified]
    print("Success Examples")
    print("Test samples: {} \n Actual Label: {} \n Predicted Label : {}\n".format(correct_classified_crabs[0:5],actual_label[0:5],predicted_label[0:5]))


visualize_failure_result(X_test, Y_test, y_pred)
visualize_success_result(X_test, Y_test, y_pred)
