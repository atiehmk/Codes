import matplotlib.pylab as plt
import numpy as np
x = np.arange(-10, 0, 0.01)
y = np.log10(1-x)
plt.plot(x, -y)
plt.title('y=-log(1-x)')
plt.xlabel('x')
plt.ylabel('y')
plt.show()
