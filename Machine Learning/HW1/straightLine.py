import numpy as np
import matplotlib.pyplot as plt
x = np.arange(0.0, 10.0, 0.01)
t0=30
t1=0.5
y = t0+t1*x
plt.plot(x, y)
plt.title("Straight Line")
plt.xlabel("x")
plt.ylabel("y")
plt.show()
