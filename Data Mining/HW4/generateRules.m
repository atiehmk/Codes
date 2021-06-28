function AssociationRules = generateRules(FrequentItemSets,mapItemsetSupport,minConf)
%Generate association rules found from the frequent
%itemsets based on the minimum confidence threshold .The Association rules are expressed as {ante} => {conseq}.
%   "FrequentItemsets" is a structure array of frequent itemsets that items are represented
%   as indices of cell array "items"
%   "mapItemsetSupport" is a Map object that maps itemsets to their support values.
%   "AssociationRules" is a structure array of association rules that meet minimum
%   confidence


    AssociationRules = struct('Ante',{},'Conseq',{},'Conf',{},'Sup',{});
    % iterate over itemset where k >= 2
    for k = 2:length(FrequentItemSets)
        % iterate over k-itemsets 
        for n = 2:size(FrequentItemSets(k).freqSets,1)
            % get one k-itemset
            freqSet = FrequentItemSets(k).freqSets(n,:);
            % get 1-item consequents from the k-itemset
            H1 = freqSet';
            % if the itemset contains more than 3 items
            if k > 2
               
                AssociationRules = aprioriGenerateRules(freqSet,H1,mapItemsetSupport,AssociationRules,minConf);
            else
                
                [~,AssociationRules] = pruneAssociationRules(freqSet,H1,mapItemsetSupport,AssociationRules,minConf);
            end
        end
    end
    
    function AssociationRules = aprioriGenerateRules(FrequentItemSets,ruleConsequents,mapItemsetSupport,AssociationRules,minConf)
    % Generate candidate rules from rule consequent
    %   "FrequentItemSets" is  frequent itemset
    %   "ruleConsequents" is a matrix that contains a rule consequents per row
    %   "mapItemsetSupport" is a Map object that maps itemsets to their support values.
    %   "AssociationRules" is a structure array that stores the rules
    %   "minConf" is the threshold to prune the rules
    
        m = size(ruleConsequents,2); % size of rule consequent
        % if frequent itemset is longer than consequent by more than 1
        if length(FrequentItemSets) > m+1
            % prune 1-item consequents by m
            if m == 1
                [~,AssociationRules] = pruneAssociationRules(FrequentItemSets,ruleConsequents,mapItemsetSupport,AssociationRules,minConf);
            end
            % use aprioriGen to generate consequents
            Hm1 = aprioriGen(ruleConsequents,m+1);
            % prune consequents by minimum confidence
            [Hm1,AssociationRules] = pruneAssociationRules(FrequentItemSets,Hm1,mapItemsetSupport,AssociationRules,minConf);
            % if we have consequents that meet the criteria, recurse until
            % we run out of such candidates
            if ~isempty(Hm1)
                AssociationRules = aprioriGenerateRules(FrequentItemSets,Hm1,mapItemsetSupport,AssociationRules,minConf);
            end
        end
    end
function Candidate_K_items = aprioriGen(freqSets, k)
%  Generate candidate k-itemsets using Apriori algorithm

    % generate candidate 2-itemsets
    if k == 2
        Candidate_K_items = combnk(freqSets,2);
    else
        % generate candidate k-itemsets (k > 2)     
        Candidate_K_items = [];
        numSets = size(freqSets,1);
        % generate two pairs of frequent itemsets to merge
        for i = 1:numSets
            for j = i+1:numSets
                % compare the first to k-2 items
                pair1 = sort(freqSets(i,1:k-2));
                pair2 = sort(freqSets(j,1:k-2));
                % if they are the same, merge
                if isequal(pair1,pair2)
                    Candidate_K_items = [Candidate_K_items; union(freqSets(i,:),freqSets(j,:))];
                end
            end
        end
    end
end



end