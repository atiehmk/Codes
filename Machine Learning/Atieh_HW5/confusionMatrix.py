# Implement confusion matrix for the prediction results of  a classifier
def func_calConfusionMatrix(predY, trueY):

    # confusion matrix result
    confusionMatrix = [[0, 0], [0, 0]]

    for i in range(len(predY)):
        if int(trueY[i]) == 1:
            if float(predY[i]) == 1:
                confusionMatrix[0][0] = confusionMatrix[0][0] + 1
            else:
                confusionMatrix[0][1] = confusionMatrix[0][1] + 1
        elif int(trueY[i]) == -1:
            if float(predY[i]) == 1:
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

