% starter codes for outline detections in HA6, CS653

load('redwine.mat','X','Y'); % X, feature matrix; Y: ratings of wine samples;  
features=X;
rating=Y;
numOfSamples = size(Y,1)
%hist(Y, 0:10);
%% step 1: generating ground-truth for outliers; 

O=zeros(size(Y)); % outlier labels. 1: outliers; 0: normal; 
O(find(Y>=8 | Y<=3))=1; %labeled as outliers





%% step 2: Please use Nearest Neighbor methods to determine outliners. 

%approach A: Data points for which there are fewer than p neighboring points within a distance D


% Array to store the determined outlier labels using approach A
O1=zeros(size(Y));

% Arrays for storing the numberOfNeigbouringPoints and average neigbouring
% points for each sample
numberOfNeigbouringPoints = zeros(numOfSamples,1);
avgNeigbouringPoints=numberOfNeigbouringPoints;

for sample=1:numOfSamples
    
        euclideanDistance = sum((repmat(features(sample,:),numOfSamples,1)-features).^2,2);
        [distance,position] = sort(euclideanDistance,'descend');
        %Average Distance between samples
        D=mean(euclideanDistance);
        %Number of neighboring points within distance D
        numberOfNeigbouringPoints(sample)=sum(euclideanDistance-D<0);
        %Average number of neighboring points within distance D
        avgNeigbouringPoints(sample)=round(mean(numberOfNeigbouringPoints));
       
 
    
end

% 
for sample=1:numOfSamples
    
        euclideanDistance = sum((repmat(features(sample,:),numOfSamples,1)-features).^2,2);
        [distance,position] = sort(euclideanDistance,'descend');

        %Number of neighboring points for each sample within distance D
        numberOfPoints(sample)=sum(euclideanDistance-D<0);

       %Determine outliers if there are fewer than p neigbouring points within
       %distance D that is the average distance between samples and assign
       %the outlier label to those points
        if  numberOfPoints(sample)<avgNeigbouringPoints(sample)  
            O1(sample)=1;
        end    
    
end



 
%  
%approach B: The top n data points whose distance to the k-th nearest neighbor is greatest

% Matrix for storing the neighrest neigbour and their distances
neighborIds = zeros(numOfSamples,50);
neighborDistances = neighborIds;

for sample=1:numOfSamples
        
        % Find the k nearest samples and store the sample index and distance 
        euclideanDistance = sum((repmat(features(sample,:),numOfSamples,1)-features).^2,2);
        [distance,position] = sort(euclideanDistance,'ascend');
        neighborIds(sample,:) = position(1:50);
        neighborDistances(sample,:) = distance(1:50);
end


% Find the top n ( n is 28 ) data points that the distance to k-nearest
% neigbour is gretaest
[V,I]=maxk(neighborDistances,28);

%Array to store the determined outlier labels using approach B
O2=zeros(size(Y));

% Assign the label of data points that the distance to k-nearest
% neigbour is gretaest as outliers
O2(neighborIds(I))=1;



  
% %approach C: The top n data points whose average distance to the k nearest neighbors is greatest

neighborIds = zeros(numOfSamples,20);
neighborDistances = neighborIds;

for sample=1:numOfSamples
    
        euclideanDistance = sum((repmat(features(sample,:),numOfSamples,1)-features).^2,2);
        [distance,position] = sort(euclideanDistance,'ascend');
        neighborIds(sample,:) = position(1:20);
        neighborDistances(sample,:) = distance(1:20);
        % Find the average distance to the k-nearest neigbour
        meanDistance(sample,:)=mean(neighborDistances(sample,:));
   
end



% Find the top n ( n is 28 ) data points that the distance to k-nearest
% neigbour is gretaest
[V2,I2]=maxk(meanDistance,28);

O3=zeros(size(Y)); 
O3(neighborIds(I2))=1;

% 
% %% step 3: Evaluate and compare the detection results of the above three methods usng confusion matrix and analyze which method works the best on this particular dataset. 
% 
% Confusion MAtrix, Accuray, Recall, and Precision 
[CM1, acc1, arrR1, arrP1]=func_confusion_matrix(O, O1);
[CM2, acc2, arrR2, arrP2]=func_confusion_matrix(O, O2);
[CM3, acc3, arrR3, arrP3]=func_confusion_matrix(O, O3);

