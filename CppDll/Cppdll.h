#pragma once
#ifdef  CPPDLL_EXPORTS
#define Cppdll_API extern "C" _declspec(dllexport)
#else
#define Cppdll_API extern "C" _declspec(dllimport)
#endif

struct ScreenRect
{
    int left;
    int top;
    int right;
    int bottom;
};


extern "C" Cppdll_API int Add(int a, int b);


extern "C" Cppdll_API int StructParamterTest(ScreenRect* sr);
