p = 0.64
n = 164
k = 73
SIMULATIONS = 5000000
totalMore = 0
import random
for j in range(0, SIMULATIONS):
    count = 0
    for i in range (0, n):
        if (random.random() < p):
            count += 1
    if (count > k):
        totalMore += 1
print(totalMore)