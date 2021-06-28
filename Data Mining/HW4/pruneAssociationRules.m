    function [prunedConsequents,AssociationRules] = pruneAssociationRules(FrequentItemSets,ruleConsequents,mapItemsetSupport,AssociationRules,minConf)
    %Calculates confidence of given rules and remove rules that don't meet
    %the minimum cofidence
    %   "FrequentItemSets" is a frequent itemset
    %   "mapItemsetSupport" is a Map object that maps itemsets to their support values.
 
    %   "AssociationRules" is a structure array that stores the rules
    %   "minConf" is the threshold to prune the rules
    %   "prunedConsequents" is a matrix of consequents that met minimum
    %   confidence
        
        % Return variable
        prunedConsequents = [];
        % iterate over the rows of ruleConsequents
        for i = 1:size(ruleConsequents,1);
            conseq = ruleConsequents(i,:);
            ante = setdiff(FrequentItemSets, conseq);
            % get support for FrequentItemSets
            supFk =mapItemsetSupport(num2str(FrequentItemSets));
            % get support for ante
            supAnte =mapItemsetSupport(num2str(ante));
            % get support for conseq
            supConseq =mapItemsetSupport(num2str(conseq));
            % calculate confidence
            conf = supFk / supAnte;
  
            if conf >= minConf
                % append the conseq to prunedConsequents
                prunedConsequents = [prunedConsequents, conseq];
                % generate a rule
                rule = struct('Ante',ante,'Conseq',conseq,...
                    'Conf',conf,'Sup',supFk);
                % append the rule
                if isempty(AssociationRules)
                    AssociationRules = rule;
                else
                    AssociationRules = [AssociationRules, rule];
                end             
            end
        end
    end
