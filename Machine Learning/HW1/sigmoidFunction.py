import matplotlib.pylab as plt
import numpy as np
import math
x = np.arange(-10, 10, 0.1)
y = 1 / (1 + np.exp(-x))
plt.plot(x, y)
plt.title('Sigmoid')
plt.xlabel('x')
plt.ylabel('y')
plt.show()
