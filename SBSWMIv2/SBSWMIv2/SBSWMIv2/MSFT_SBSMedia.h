/* @migen@ */
/*
**==============================================================================
**
** WARNING: THIS FILE WAS AUTOMATICALLY GENERATED. PLEASE DO NOT EDIT.
**
**==============================================================================
*/
#ifndef _MSFT_SBSMedia_h
#define _MSFT_SBSMedia_h

#include <MI.h>

/*
**==============================================================================
**
** MSFT_SBSMedia [MSFT_SBSMedia]
**
** Keys:
**
**==============================================================================
*/

typedef struct _MSFT_SBSMedia
{
    MI_Instance __instance;
    /* MSFT_SBSMedia properties */
}
MSFT_SBSMedia;

typedef struct _MSFT_SBSMedia_Ref
{
    MSFT_SBSMedia* value;
    MI_Boolean exists;
    MI_Uint8 flags;
}
MSFT_SBSMedia_Ref;

typedef struct _MSFT_SBSMedia_ConstRef
{
    MI_CONST MSFT_SBSMedia* value;
    MI_Boolean exists;
    MI_Uint8 flags;
}
MSFT_SBSMedia_ConstRef;

typedef struct _MSFT_SBSMedia_Array
{
    struct _MSFT_SBSMedia** data;
    MI_Uint32 size;
}
MSFT_SBSMedia_Array;

typedef struct _MSFT_SBSMedia_ConstArray
{
    struct _MSFT_SBSMedia MI_CONST* MI_CONST* data;
    MI_Uint32 size;
}
MSFT_SBSMedia_ConstArray;

typedef struct _MSFT_SBSMedia_ArrayRef
{
    MSFT_SBSMedia_Array value;
    MI_Boolean exists;
    MI_Uint8 flags;
}
MSFT_SBSMedia_ArrayRef;

typedef struct _MSFT_SBSMedia_ConstArrayRef
{
    MSFT_SBSMedia_ConstArray value;
    MI_Boolean exists;
    MI_Uint8 flags;
}
MSFT_SBSMedia_ConstArrayRef;

MI_EXTERN_C MI_CONST MI_ClassDecl MSFT_SBSMedia_rtti;

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_Construct(
    _Out_ MSFT_SBSMedia* self,
    _In_ MI_Context* context)
{
    return MI_Context_ConstructInstance(context, &MSFT_SBSMedia_rtti,
        (MI_Instance*)&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_Clone(
    _In_ const MSFT_SBSMedia* self,
    _Outptr_ MSFT_SBSMedia** newInstance)
{
    return MI_Instance_Clone(
        &self->__instance, (MI_Instance**)newInstance);
}

MI_INLINE MI_Boolean MI_CALL MSFT_SBSMedia_IsA(
    _In_ const MI_Instance* self)
{
    MI_Boolean res = MI_FALSE;
    return MI_Instance_IsA(self, &MSFT_SBSMedia_rtti, &res) == MI_RESULT_OK && res;
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_Destruct(_Inout_ MSFT_SBSMedia* self)
{
    return MI_Instance_Destruct(&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_Delete(_Inout_ MSFT_SBSMedia* self)
{
    return MI_Instance_Delete(&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_Post(
    _In_ const MSFT_SBSMedia* self,
    _In_ MI_Context* context)
{
    return MI_Context_PostInstance(context, &self->__instance);
}

/*
**==============================================================================
**
** MSFT_SBSMedia.EnableMediaServer()
**
**==============================================================================
*/

typedef struct _MSFT_SBSMedia_EnableMediaServer
{
    MI_Instance __instance;
    /*OUT*/ MI_ConstUint32Field MIReturn;
    /*IN*/ MI_ConstStringField CommandLine;
}
MSFT_SBSMedia_EnableMediaServer;

MI_EXTERN_C MI_CONST MI_MethodDecl MSFT_SBSMedia_EnableMediaServer_rtti;

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Construct(
    _Out_ MSFT_SBSMedia_EnableMediaServer* self,
    _In_ MI_Context* context)
{
    return MI_Context_ConstructParameters(context, &MSFT_SBSMedia_EnableMediaServer_rtti,
        (MI_Instance*)&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Clone(
    _In_ const MSFT_SBSMedia_EnableMediaServer* self,
    _Outptr_ MSFT_SBSMedia_EnableMediaServer** newInstance)
{
    return MI_Instance_Clone(
        &self->__instance, (MI_Instance**)newInstance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Destruct(
    _Inout_ MSFT_SBSMedia_EnableMediaServer* self)
{
    return MI_Instance_Destruct(&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Delete(
    _Inout_ MSFT_SBSMedia_EnableMediaServer* self)
{
    return MI_Instance_Delete(&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Post(
    _In_ const MSFT_SBSMedia_EnableMediaServer* self,
    _In_ MI_Context* context)
{
    return MI_Context_PostInstance(context, &self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Set_MIReturn(
    _Inout_ MSFT_SBSMedia_EnableMediaServer* self,
    _In_ MI_Uint32 x)
{
    ((MI_Uint32Field*)&self->MIReturn)->value = x;
    ((MI_Uint32Field*)&self->MIReturn)->exists = 1;
    return MI_RESULT_OK;
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Clear_MIReturn(
    _Inout_ MSFT_SBSMedia_EnableMediaServer* self)
{
    memset((void*)&self->MIReturn, 0, sizeof(self->MIReturn));
    return MI_RESULT_OK;
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Set_CommandLine(
    _Inout_ MSFT_SBSMedia_EnableMediaServer* self,
    _In_z_ const MI_Char* str)
{
    return self->__instance.ft->SetElementAt(
        (MI_Instance*)&self->__instance,
        1,
        (MI_Value*)&str,
        MI_STRING,
        0);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_SetPtr_CommandLine(
    _Inout_ MSFT_SBSMedia_EnableMediaServer* self,
    _In_z_ const MI_Char* str)
{
    return self->__instance.ft->SetElementAt(
        (MI_Instance*)&self->__instance,
        1,
        (MI_Value*)&str,
        MI_STRING,
        MI_FLAG_BORROW);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSMedia_EnableMediaServer_Clear_CommandLine(
    _Inout_ MSFT_SBSMedia_EnableMediaServer* self)
{
    return self->__instance.ft->ClearElementAt(
        (MI_Instance*)&self->__instance,
        1);
}

/*
**==============================================================================
**
** MSFT_SBSMedia provider function prototypes
**
**==============================================================================
*/

/* The developer may optionally define this structure */
typedef struct _MSFT_SBSMedia_Self MSFT_SBSMedia_Self;

MI_EXTERN_C void MI_CALL MSFT_SBSMedia_Load(
    _Outptr_result_maybenull_ MSFT_SBSMedia_Self** self,
    _In_opt_ MI_Module_Self* selfModule,
    _In_ MI_Context* context);

MI_EXTERN_C void MI_CALL MSFT_SBSMedia_Unload(
    _In_opt_ MSFT_SBSMedia_Self* self,
    _In_ MI_Context* context);

MI_EXTERN_C void MI_CALL MSFT_SBSMedia_Invoke_EnableMediaServer(
    _In_opt_ MSFT_SBSMedia_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_opt_z_ const MI_Char* methodName,
    _In_ const MSFT_SBSMedia* instanceName,
    _In_opt_ const MSFT_SBSMedia_EnableMediaServer* in);


#endif /* _MSFT_SBSMedia_h */
