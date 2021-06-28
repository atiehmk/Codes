
%% HA6, PART-II, Use PageRank Algorithm to Rank Websites

% This example shows how to use a PageRank algorithm to rank a collection
% of websites. Although the PageRank algorithm was originally designed to
% rank search engine results, it also can be more broadly applied to the
% nodes in many different types of graphs. The PageRank score gives an
% idea of the relative importance of each graph node based on how it is
% connected to the other nodes.
%
 
% PLEASE REPLACE the PLACEHOLDER LINES with your own codes. There are two
% placeholders. 
 

%%  Step 1 load the adjacent matrix; 
load('webpages.mat', 'A','U') ;
spy(A)
%  normalize this matrix
%% PLACEHOLDER-Start

%Create the Adjacency matrix 
adjacent_matrix=full(A);
% calculate out-degree, sum of each row
r = sum(adjacent_matrix,2);
% calculate in-degree, sum of each column
c = sum(adjacent_matrix,1);
%Normalize matrix
normal_adjacent_matrix = adjacent_matrix*diag(1./c);

%PLACEHOLDER-End

% visualize the graph 
% Create a directed graph with the sparse adjacency matrix, |A|, using the
% URLs contained in |U| as node names.
G = digraph(A,U);
% Plot the graph using the force layout.
plot(G,'NodeLabel',{},'NodeColor',[0.93 0.78 0],'Layout','force');
title('Entire network of Websites'); 

%% step 2: Compute the PageRank scores for the graph, |G|, using 200 iterations and
% a damping factor of |0.85|.
d=0.85;
pr=ones(length(U),1); % initial ranks 
e=ones(100);

%% PLACEHOLDER-Start
% update ranks according to adjacent matrix; 
for iter=1:200          
    
    pr = (1-d)*e+d*normal_adjacent_matrix'*pr; 

end

%PLACEHOLDER-End

% you might compare your results with this line: 
% pr = centrality(G,'pagerank','MaxIterations',200,'FollowProbability',0.85);
 
%% step 3: visualize the webaites with higher rank. 

% Extract and plot a subgraph containing all nodes whose score is greater
% than half of the maximal rank value. Color the graph nodes based on their PageRank score.
H = subgraph(G,find(pr > 0.5*max(pr(:))));
figure, plot(H,'NodeLabel',{},'Layout','force');
title('high-ranked Websites')
colorbar


