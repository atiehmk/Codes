I = [2 3 1 3 4 4 1];
J = [1 1 2 2 2 3 4];

% Create the Adjacency matrix/Connectivity matrix from I,J
H = full(sparse(I,J,1,4,4)) % notice the order of

% if you are using MATLAB 2015b or later, you can plot the directed graph
G = digraph(H'); plot(G); axis off % notice the transpose on H

% calculate in-degree and out-degree
r = sum(H,2);  % out-degree, sum of each row
c = sum(H,1);  % in-degree, sum of each column

% create the scaled matrix Ht
Ht = H*diag(1./c);

% Find the PageRank vector from eigenvalues associated with the eigenvector
[V,D] = eig(Ht);    % find the eigenvalues and the eigenvectors
[~,ind] = min(abs(diag(D)-1));  % find the index of the eigenvalue 1 (not exactly because of numerical error)
p = V(:,1)/sum(V(:,1))         % normalised eigenvector as the PageRank vector
[newp,rank] = sort(p,'descend') % sort in descending order