import matplotlib.pylab as plt
import numpy as np
x = np.arange(1, 10, 0.01)
f = np.log10(x)
plt.plot(x, -f)
plt.xlabel('x')
plt.ylabel('y')
plt.title('y=-log(x)')
plt.show()
