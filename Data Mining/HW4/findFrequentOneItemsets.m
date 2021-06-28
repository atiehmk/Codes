 function [Frequent_1_itemsets,mapItemsetSupport,itemsetCandidate]= findFrequentOneItemsets(trafficDataT,numberOfData,minSup)
    % Generate the frequent 1-itemsets from 1-itemset table and pruned with the minimum support threshold 
    
        % 1-itemset candidates
        itemsetCandidate = trafficDataT.Key;
        % 1-itemset candidates count
        itemsetCandidateCount = cell2mat(trafficDataT.Value);
        % calculate support for all candidates
        sup = itemsetCandidateCount ./numberOfData;
        % create a map object and store support data
        mapItemsetSupport = containers.Map();
        for j = 1:length(itemsetCandidate)
            mapItemsetSupport(num2str(j)) = sup(j);
        end
        % Prune candidates itemsets by minSupport
        freqSet = find(sup >= minSup);
        % Save result 
        Frequent_1_itemsets = struct('freqSets',freqSet);
    end
