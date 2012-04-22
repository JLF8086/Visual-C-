import itertools
import math

def combs(p, n):
    return math.factorial(n)/(math.factorial(p)*math.factorial(n-p))

p = 0.045
n = 57
k = 5
result = 0

for i in range(0, k+1):
    print i
    result += p**i*(1-p)**(n-i)*combs(i, n)

print result
