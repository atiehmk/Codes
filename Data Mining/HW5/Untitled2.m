load('webpages.mat', 'A','U') ;
spy(A)
%  normalize this matrix
%% PLACEHOLDER-Start

%Create the Adjacency matrix 
M=full(A);
% Tweak the graph to add weak links between all nodes. 
beta = 0.85;
n = size(M, 1);

A = beta * M + ((1 - beta) * (1 / n) * ones(n, n))
% Initialize the scores to 1 / the number of nodes.
r = ones(100, 1) * 1/100;

% Use the Power Iteration method to find the principal eigenvector.
iterNum = 1;

while (true)
    % Store the previous values of r.
    rp = r;
 
    % Calculate the next iteration of r.
    r = M * r;
 
    fprintf('Iteration %d, r =\n', iterNum);
    r
 
    % Break when r stops changing.
    if ~any(abs(rp - r) > 0.00001)
        break
    end
 
    iterNum = iterNum + 1;
end

% We can also verify that M*r gives back r...
fprintf('M * r =\n');
M * r

H = subgraph(G,find(r > 0.5*max(r(:))));
figure, plot(H,'NodeLabel',{},'Layout','force');
title('high-ranked Websites')
colorbar


% M = ...
% [  0.09091   0.00000   0.00000   0.50000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000;
%    0.09091   0.00000   1.00000   0.50000   0.33333   0.50000   0.50000   0.50000   0.50000   0.00000   0.00000;
%    0.09091   1.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000;
%    0.09091   0.00000   0.00000   0.00000   0.33333   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000;
%    0.09091   0.00000   0.00000   0.00000   0.00000   0.50000   0.50000   0.50000   0.50000   1.00000   1.00000;
%    0.09091   0.00000   0.00000   0.00000   0.33333   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000;
%    0.09091   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000;
%    0.09091   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000;
%    0.09091   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000;
%    0.09091   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000;
%    0.09091   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000   0.00000];
% 
% N = length(M);
% PR = (1/N)*ones(length(M),1);   %define PageRank vector for t = 0
% d = 0.85;                       %define damping rate
% iter = 1;
% delta_PR = Inf;                 %set initial error to infinity
% while delta_PR > 1e-6           %iterate until error is less than 1e-6
%     tic;
% 
%     prev_PR = PR;               %save previous PageRank vector (t-1)
%     PR = d*M*PR + ((1-d)/N)*ones(N,1);  %calculate new PageRank (t)
% 
%     delta_PR = norm(PR-prev_PR);%calculate new error
%     t(iter)=toc;
%     str=sprintf('for d=%g , iteration %d: time=%11.4g',delta_PR,iter,t(iter));
%     disp(str);
%     iter = iter + 1;
% end
% 
% powerRank = pinv((eye(length(M)) - d*M))*(((1-d)/N)*ones(length(M),1));