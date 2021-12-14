#pragma once
#ifdef  CPPDLL_EXPORTS
#define Cppdll_API extern "C" _declspec(dllexport)
#else
#define Cppdll_API extern "C" _declspec(dllimport)
#endif

extern "C" Cppdll_API int Add(int a ,int b);
