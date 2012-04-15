p = 0.8
q = 0.6
m = 6
n = 6
SIMULATIONS = 1000000

import random
lister = []
for k in range(0, 2):
    istsuccess = 0
    for i in range(0, SIMULATIONS):
        ist = 0
        geo = 0
        for j in range(0, m):
            if (p > random.random()):
                ist = ist + 1
        for j in range(0, n):
            if (q > random.random()):
                geo = geo + 1
        if (ist > geo):
            istsuccess = istsuccess + 1
    lister.append((istsuccess*1.0/SIMULATIONS))
    print(istsuccess*1.0/SIMULATIONS)
print(sum(lister)*1.0/len(lister))
print(min(lister))
print(max(lister))