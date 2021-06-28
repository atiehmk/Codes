function Candidate_k_itemsets = generateItemsets(freqSets, k)
% Generate candidate k-itemsets 
%   This function implements F_k-1 x F_k-1 method, which merges two pairs
%   (k-1)-itemsets to generate new k-itemsets if the first (k-2) items are
%   identical between the pair.
%


    % generate candidate 2-itemsets
    if k == 2
        Candidate_k_itemsets = combnk(freqSets,2);
    else
        % generate candidate k-itemsets (k > 2)     
        Candidate_k_itemsets = [];
        numSets = size(freqSets,1);
        % generate two pairs of frequent itemsets to merge
        for i = 1:numSets
            for j = i+1:numSets
                % compare the first to k-2 items
                pair1 = sort(freqSets(i,1:k-2));
                pair2 = sort(freqSets(j,1:k-2));
                % Merge if they are the same
                if isequal(pair1,pair2)
                    Candidate_k_itemsets = [Candidate_k_itemsets; union(freqSets(i,:),freqSets(j,:))];
                end
            end
        end
    end
end