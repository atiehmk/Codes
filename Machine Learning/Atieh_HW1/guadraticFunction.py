import numpy as np
import matplotlib.pyplot as plt
x = np.arange(-20.0, 20.0, 1.0) #get values between -10 and 10 with 0.01 step
t0= 20
t1= 25
y = (x-t1)**2 + t0 
plt.plot(x, y)
plt.xlabel('x')
plt.ylabel('y')
plt.title('Quadratic function')
plt.show()
