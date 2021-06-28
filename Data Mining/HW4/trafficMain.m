clc;clearvars;
close all;

% Binarize categorical attributes ( Using two varibales Yes and No )
Weather_Condition_Good={'Yes';'No';'Yes';'Yes';'No';'Yes';'No';'Yes';'Yes';'No';'Yes';'No'};
Driver_Condition_Sober={'No';'Yes';'Yes';'Yes';'Yes';'No';'No';'Yes';'No';'Yes';'No';'Yes'};
Trafic_Violation_Exceed_Speed_Limit={'Yes';'No';'No';'Yes';'No';'No';'No';'No';'No';'No';'Yes';'No'};
Trafic_Violation_Disobey_traffic_Signal={'No';'No';'No';'No';'Yes';'No';'No';'Yes';'No';'Yes';'No';'No'};
Trafic_Violation_Disobey_Stop_Sign= {'No';'No';'Yes';'No';'No';'Yes';'No';'No';'No';'No';'No';'Yes'};
Seat_Belt={'No';'Yes';'Yes';'Yes';'No';'Yes';'Yes';'Yes';'No';'No';'Yes';'Yes'};

%Crash Severity classification
Crash_Severity={'Major';'Minor';'Minor';'Major';'Major';'Minor';'Major';'Major';'Major';'Major';'Major';'Minor'};

% Create table 
T=table(Crash_Severity,Weather_Condition_Good,Driver_Condition_Sober,Trafic_Violation_Exceed_Speed_Limit,Trafic_Violation_Disobey_traffic_Signal,Trafic_Violation_Disobey_Stop_Sign,Seat_Belt);
header={'Crash_Severity','Weather_Condition_Good ','Driver_Condition_Sober','Trafic_Violation_Exceed_Speed_Limit','Trafic_Violation_Disobey_traffic_Signal','Trafic_Violation_Disobey_Stop_Sign','Seat_Belt'};
T.Properties.VariableNames = header


data = table2array(T);

% Extract the attributes from table
attributes = strcmp(data(:,2:end), 'Yes');
attributes = [attributes, strcmp(data(:,2:end), 'No')];
major = strcmp(data(:,1),'Major');
minor = strcmp(data(:,1),'Minor');

attributes= [major,minor,attributes]


trafficData = cell(size(major));
for i = 1:length(major)
    trafficData{i} = find(attributes(i,:));
end


trafficAttributes = {'Major',...
    'Minor',...
    'Weather_Condition_Good = Yes'...
    'Driver_Condition_Sober = Yes',...
    'Trafic_Violation_Exceed_Speed_Limit = Yes',...
    'Trafic_Violation_Disobey_traffic_Signal = Yes',...
    'Trafic_Violation_Disobey_Stop_Sign = Yes',...
    'Seat_Belt = Yes',...
    'Weather_Condition_Good =No',...
    'Driver_Condition_Sober = No',...
    'Trafic_Violation_Exceed_Speed_Limit = No',...
    'Trafic_Violation_Disobey_traffic_Signal = No',...
    'Trafic_Violation_Disobey_Stop_Sign = No',...
    'Seat_Belt = No',...
    };




minSup = 0.3; 

[FrequentItems,Support,items] = findFrequentItemsets(trafficData,minSup);

fprintf('Number of Frequent Itemsets: %d\n', sum(arrayfun(@(x) length(x.freqSets), FrequentItems)))



% Display frequent itemsets 
for i = 1
for j = 1:length(FrequentItems(i).freqSets)
   
 disp(['{', sprintf('%s, %s, %s, %s, %s, %s, %s',trafficAttributes{FrequentItems(i).freqSets(j,1)}),'}'])
 
end
end


for i = 2
for j = 1:length(FrequentItems(i).freqSets)

   
 disp(['{', sprintf('%s, %s, %s, %s, %s, %s, %s',trafficAttributes{FrequentItems(i).freqSets(j,1)},trafficAttributes{FrequentItems(i).freqSets(j,2)} ),'}'])
 
end
end

for i = 3
for j = 1:length(FrequentItems(i).freqSets)

 disp(['{', sprintf('%s, %s, %s, %s, %s, %s, %s',trafficAttributes{FrequentItems(i).freqSets(j,1)},trafficAttributes{FrequentItems(i).freqSets(j,2)},trafficAttributes{FrequentItems(i).freqSets(j,3)}  ),'}'])
 
end
end

for i = 4
for j = 1:length(FrequentItems(i).freqSets)-1

    
 disp(['{', sprintf('%s, %s, %s, %s, %s, %s, %s',trafficAttributes{FrequentItems(i).freqSets(j,1)},trafficAttributes{FrequentItems(i).freqSets(j,2)},trafficAttributes{FrequentItems(i).freqSets(j,3)},trafficAttributes{FrequentItems(i).freqSets(j,4)}  ),'}'])
 

end
end


% Generate rules 
minConfidence = 0.6; % minimum confidence threshold 0.9
rules = generateRules(FrequentItems,Support,minConfidence);
fprintf('Minimum Confidence     : %.2f\n', minConfidence)
fprintf('Number of Rules Found            : %d\n\n', length(rules))



% Get the rules with 1-item conseq with Crash Severity classification
major = rules(arrayfun(@(x) isequal(x.Conseq,1),rules));
minor = rules(arrayfun(@(x) isequal(x.Conseq,2),rules));



crashSeverity = [major,minor];

%%%%%%%%%%%%%%%%%%%%%%

%Disply Rules
for i = 1:length(crashSeverity)
          disp(['{', sprintf('%s, %s, %s,',trafficAttributes{crashSeverity(i).Ante}),'} => ',...
        sprintf('{%s}', trafficAttributes{crashSeverity(i).Conseq})])
       
end
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%



