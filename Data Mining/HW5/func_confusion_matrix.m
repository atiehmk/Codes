function [CM, acc, arrR, arrP]=func_confusion_matrix(teY, hatY)
% this function is used to calculate the confusion matrix and a set of
% metrics. 
% INPUT: 
%testY, ground-truth lables;
%hatY, predicted labels; 
%OUTPUT
%CM, confuction matrix
%acc, accuracy
%arrR[], per-class recall rate,
%arrP[], per-class prediction rate. 

%% your codes for creating confusion matrix; 
            
            unique_actual_class=unique(teY) % Get the uniques labels for the classes
            class_list=unique_actual_class;
            numOfClass=length(unique_actual_class);
            confusion_matrix=zeros(numOfClass);
            Predicted_Class=cell(1,numOfClass);
            row=cell(1,numOfClass);
            %Calculate conufsion matrix for all classes with varying number
            %of labels
            for i=1:numOfClass
                for j=1:numOfClass
                    val=(teY==class_list(i)) & (hatY==class_list(j));
                    confusion_matrix(i,j)=sum(val);
                    Predicted_Class{i,j}=sum(val);
                end
                row{i}=strcat('Actual Class ',num2str(i));
            end
            
            c_matrix_table=cell2table(Predicted_Class);
            c_matrix_table.Properties.RowNames=row;
            disp('________________')
            disp('Confusion Matrix')
            disp(c_matrix_table)
            CM=confusion_matrix

%% your codes for calcuating acc; %% your codes for calcualting arrR and arrP; 
            [row,col]=size(confusion_matrix);
            numOfClass=row;
            switch numOfClass
                case 2
                    TP=confusion_matrix(1,1);
                    FN=confusion_matrix(1,2);
                    FP=confusion_matrix(2,1);
                    TN=confusion_matrix(2,2);
                    
                otherwise
                    TP=zeros(1,numOfClass);
                    FN=zeros(1,numOfClass);
                    FP=zeros(1,numOfClass);
                    TN=zeros(1,numOfClass);
                    for i=1:numOfClass
                        TP(i)=confusion_matrix(i,i);
                        FN(i)=sum(confusion_matrix(i,:))-confusion_matrix(i,i);
                        FP(i)=sum(confusion_matrix(:,i))-confusion_matrix(i,i);
                        TN(i)=sum(confusion_matrix(:))-TP(i)-FP(i)-FN(i);
                    end
                    
            end
            P=TP+FN;
            N=FP+TN;
            switch numOfClass
                case 2
                    accuracy=(TP+TN)/(P+N)
                    acc=accuracy
                    Recall_class1=TP/(TP+FN);
                    Recall_class2=TN/(FP+TN);
                    arrR(1,1)=Recall_class1;
                    arrR(1,2)=Recall_class2
                    Precision_class1=TP/(TP+FP);
                    Precision_class2=TN/(FP+TN);
                    arrP(1,1)=Precision_class1;
                    arrP(1,2)=Precision_class2
                    
                                   
                otherwise
                    accuracy=(TP)./(P+N);
                    accuracy=sum(accuracy);
                    acc=accuracy 
                    Recall=TP./P;
                    arrR=Recall
        
                   Precision=TP./(TP+FP);
                   arrP=Precision
            
            end   
            
        


