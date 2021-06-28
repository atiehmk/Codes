function [FrequentItemsets,mapItemsetSupport,items] = findFrequentItemsets(trafficData,minSupport,oneItemsets)
% Find frequent itemsets 
%   "items" is a cell array of unique items
%   "oneItemSets" is a table that list all single items that
%    appear in trafficData along with their frequencies 
%   "FrequentItemsets" is a structure array of frequent itemsets that items are represented
%   as indices of cell array "items"
%   "mapItemsetSupport" is a Map object that maps itemsets to their support values.


    if iscell(trafficData)
        trafficData = table(num2cell(1:length(trafficData))',trafficData,'VariableNames',{'Key','Value'});
       
    end
    
    % Check the number of inputs of function 
    % Get the unique items and their indices
    if nargin == 2
        items = trafficData.Value;
        [uniqItems,~,idx] = unique([items{:}]');
        oneItemsets = table(uniqItems,num2cell(accumarray(idx,1)),'VariableNames',{'Key','Value'});
    end
    
    % Number of trafficData
    numberOfData= height(trafficData);
 
    trafficData = sortrows(trafficData,'Key');
    oneItemsets = sortrows(oneItemsets,'Key');
    
    %Frequent 1-itemsets
    [FrequentItemsets,mapItemsetSupport,items] = findFrequentOneItemsets(oneItemsets,numberOfData,minSupport);

    if isempty(FrequentItemsets.freqSets)
        fprintf('No frequent itemset found at minSup = %.2f\n',minSupport)
        return
    end
    
    % Get frequent k-itemsets ( k >= 2 )
    k = 2;
    while true
        % Generate candidate itemsets
        candidateItemsets = generateItemsets(FrequentItemsets(k-1).freqSets, k);
        % Prune candidates that are below minimum support threshold
        [FrequentKItemsets, support] = pruneCandidates(trafficData,candidateItemsets,numberOfData,items,minSupport);

        % update support data; if empty, exit the loop
        if ~isempty(support)
            % create a map object to store suppor data
            mapS = containers.Map();
            for i = 1:length(support)
                mapS(num2str(candidateItemsets(i,:))) = support(i);
            end
            % update Support
            mapItemsetSupport = [mapItemsetSupport; mapS];
        else
            break;
        end
        % save the frequent itemsets above minSup
        if ~isempty(FrequentKItemsets)
            FrequentItemsets(k).freqSets = FrequentKItemsets;
          
            k = k + 1;
        else
            break;
        end
    end
    

    function [Frequent_k_itemsets,support] = pruneCandidates(traficDataT,candidateItemsets,numberOfData,items,minSupport)
    %Prune candidate itemsets to generate frequent k-itemsets 
    
  
        % calculate support count for all candidates
        support = zeros(size(candidateItemsets,1),1);
        % for each transaction
        for l = 1:numberOfData
            % get the item idices
            t = find(ismember(items, traficDataT.Value{l}));
            % increment the support count
            support(all(ismember(candidateItemsets,t),2)) = support(all(ismember(candidateItemsets,t),2)) + 1;
        end
        % calculate support
        support = support./numberOfData;
        
        % return the candidates that meet the criteria
        Frequent_k_itemsets = candidateItemsets(support >= minSupport,:);
    end

end

