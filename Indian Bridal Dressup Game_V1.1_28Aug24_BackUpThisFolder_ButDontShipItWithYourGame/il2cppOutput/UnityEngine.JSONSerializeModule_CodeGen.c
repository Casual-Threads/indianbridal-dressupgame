﻿#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.String UnityEngine.JsonUtility::ToJsonInternal(System.Object,System.Boolean)
extern void JsonUtility_ToJsonInternal_mB893BE1511779B2E36B24BC77D2FB52BF5894CDD (void);
// 0x00000002 System.Object UnityEngine.JsonUtility::FromJsonInternal(System.String,System.Object,System.Type)
extern void JsonUtility_FromJsonInternal_m6C8155071DFF33D870873F945D1E4C965D1FE6C0 (void);
// 0x00000003 System.String UnityEngine.JsonUtility::ToJson(System.Object)
extern void JsonUtility_ToJson_m28CC6843B9D3723D88AD13EA3829B71FDE7826BA (void);
// 0x00000004 System.String UnityEngine.JsonUtility::ToJson(System.Object,System.Boolean)
extern void JsonUtility_ToJson_m53A1FEE0D388CF3A629E093C04B5E1A6D5463B53 (void);
// 0x00000005 System.Void UnityEngine.JsonUtility::FromJsonOverwrite(System.String,System.Object)
extern void JsonUtility_FromJsonOverwrite_mF60C8238431C1A42F7F482BB717757B281570D56 (void);
static Il2CppMethodPointer s_methodPointers[5] = 
{
	JsonUtility_ToJsonInternal_mB893BE1511779B2E36B24BC77D2FB52BF5894CDD,
	JsonUtility_FromJsonInternal_m6C8155071DFF33D870873F945D1E4C965D1FE6C0,
	JsonUtility_ToJson_m28CC6843B9D3723D88AD13EA3829B71FDE7826BA,
	JsonUtility_ToJson_m53A1FEE0D388CF3A629E093C04B5E1A6D5463B53,
	JsonUtility_FromJsonOverwrite_mF60C8238431C1A42F7F482BB717757B281570D56,
};
static const int32_t s_InvokerIndices[5] = 
{
	5173,
	4712,
	5592,
	5173,
	5333,
};
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_UnityEngine_JSONSerializeModule_CodeGenModule;
const Il2CppCodeGenModule g_UnityEngine_JSONSerializeModule_CodeGenModule = 
{
	"UnityEngine.JSONSerializeModule.dll",
	5,
	s_methodPointers,
	0,
	NULL,
	s_InvokerIndices,
	0,
	NULL,
	0,
	NULL,
	0,
	NULL,
	NULL,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
