#include "pch.h"
#include "Cppdll.h"
#include <iostream>
using namespace std;

Cppdll_API int Add(int a, int b)
{
    return a + b;
}

Cppdll_API int StructParamterTest(ScreenRect* sr)
{
    int x = sr->bottom + sr->left + sr->right + sr->top;

    cout << "Value of x is : " << x << endl;

    return x;
}
