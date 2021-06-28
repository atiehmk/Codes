% cs653, data mining, HA3. 
% This routine is used to implement the K Nearest Neighbor (KNN) method and
% use it to classify flower samples in the Iris dataset. 

%There are 3 major steps. 

% 
K= 10;  %number of nearest neighbors used for voting. 
CM=zeros(3,3); %confusion matrix; 
acc=0; % accuracy
arrR=zeros(1,3); % per-class recall rate; 
arrP=zeros(1,3); % per-class precision rate; 

%% Step 1.	Load data and split the samples into two subsets
%one for training, the other for testing. 
load('iris_matrix.mat','X');

D=randperm(150);
%training
trX=X(D(1:100), 1:4); %training samples
trY=X(D(1:100), 5); % training labels;
teX=X(D(101:end), 1:4); %testing samples; 
teY=X(D(101:end),5); %testing labels;

%% 2.	for each testing sample, calculate its distances to every training sample; 
hatY=zeros(50,1); % predicted classes


numOfTestingData = size(teX,1);

numOfTrainingData = size(trX,1);

for sample=1:numOfTestingData
    
        euclideandistance = sum((repmat(teX(sample,:),numOfTrainingData,1)-trX).^2,2);
       
       % Find the top k nearest samples and store them in an array
        [distance,position] = sort(euclideandistance,'ascend');
        knearestneighbors=position(1:K);
        knearestdistances=distance(1:K);
      

        % Vote to predict the class of the testing sample  
        for i=1:K
            A(i) = trY(knearestneighbors(i)); 
            
        end  
        Mode = mode(A);

        if (Mode~=1)
            hatY(sample) = mode(A);
        else 
            hatY(sample) = trY(knearestneighbors(1));
        end
        
        
      
        
end

[CM, acc, arrR, arrP]=func_confusion_matrix(teY, hatY);


