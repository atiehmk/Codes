import numpy as np
import download_data as dl
import matplotlib.pyplot as plt
import sklearn.svm as svm
from sklearn import metrics
from conf_matrix import func_confusion_matrix
import numpy
import pickle

# CS 596, machine learning

## step 1: load data from csv file.
data = dl.download_data('crab.csv').values

n = 200
# split data
S = np.random.permutation(n)
# 100 training samples
Xtr = data[S[:100], :6]
Ytr = data[S[:100], 6:]



print(Xtr)
print(Ytr)
# 100 testing samples
X_test = data[S[100:], :6]
Y_test = data[S[100:], 6:].ravel()



## step 2 randomly split Xtr/Ytr into two even subsets: use one for training, another for validation.
#############placeholder: training/validation #######################
n2 = len(Xtr)
S2 = np.random.permutation(n2)

# subsets for training models
x_train = Xtr[S2[:50]]
y_train = Ytr[S2[:50]].ravel()

# subsets for validation
x_validation = Xtr[S2[50:]]
y_validation = Ytr[S2[50:]].ravel()


# storing the training , validation and testing data

pickle.dump(x_train, open("xTrain.dat", "wb"))
pickle.dump(y_train, open("yTrain.dat", "wb"))


pickle.dump(x_validation, open("xValidation.dat", "wb"))
pickle.dump(y_validation, open("yValidation.dat", "wb"))


pickle.dump(X_test, open("xTest.dat", "wb"))
pickle.dump(Y_test, open("yTest.dat", "wb"))
