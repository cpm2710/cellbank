/* @migen@ */
/*
**==============================================================================
**
** WARNING: THIS FILE WAS AUTOMATICALLY GENERATED. PLEASE DO NOT EDIT.
**
**==============================================================================
*/
#ifndef _MSFT_SBSUser_h
#define _MSFT_SBSUser_h

#include <MI.h>

/*
**==============================================================================
**
** MSFT_SBSUser [MSFT_SBSUser]
**
** Keys:
**    UserName
**
**==============================================================================
*/

typedef struct _MSFT_SBSUser
{
    MI_Instance __instance;
    /* MSFT_SBSUser properties */
    /*KEY*/ MI_ConstStringField UserName;
    MI_ConstStringField PassWord;
}
MSFT_SBSUser;

typedef struct _MSFT_SBSUser_Ref
{
    MSFT_SBSUser* value;
    MI_Boolean exists;
    MI_Uint8 flags;
}
MSFT_SBSUser_Ref;

typedef struct _MSFT_SBSUser_ConstRef
{
    MI_CONST MSFT_SBSUser* value;
    MI_Boolean exists;
    MI_Uint8 flags;
}
MSFT_SBSUser_ConstRef;

typedef struct _MSFT_SBSUser_Array
{
    struct _MSFT_SBSUser** data;
    MI_Uint32 size;
}
MSFT_SBSUser_Array;

typedef struct _MSFT_SBSUser_ConstArray
{
    struct _MSFT_SBSUser MI_CONST* MI_CONST* data;
    MI_Uint32 size;
}
MSFT_SBSUser_ConstArray;

typedef struct _MSFT_SBSUser_ArrayRef
{
    MSFT_SBSUser_Array value;
    MI_Boolean exists;
    MI_Uint8 flags;
}
MSFT_SBSUser_ArrayRef;

typedef struct _MSFT_SBSUser_ConstArrayRef
{
    MSFT_SBSUser_ConstArray value;
    MI_Boolean exists;
    MI_Uint8 flags;
}
MSFT_SBSUser_ConstArrayRef;

MI_EXTERN_C MI_CONST MI_ClassDecl MSFT_SBSUser_rtti;

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Construct(
    _Out_ MSFT_SBSUser* self,
    _In_ MI_Context* context)
{
    return MI_Context_ConstructInstance(context, &MSFT_SBSUser_rtti,
        (MI_Instance*)&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Clone(
    _In_ const MSFT_SBSUser* self,
    _Outptr_ MSFT_SBSUser** newInstance)
{
    return MI_Instance_Clone(
        &self->__instance, (MI_Instance**)newInstance);
}

MI_INLINE MI_Boolean MI_CALL MSFT_SBSUser_IsA(
    _In_ const MI_Instance* self)
{
    MI_Boolean res = MI_FALSE;
    return MI_Instance_IsA(self, &MSFT_SBSUser_rtti, &res) == MI_RESULT_OK && res;
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Destruct(_Inout_ MSFT_SBSUser* self)
{
    return MI_Instance_Destruct(&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Delete(_Inout_ MSFT_SBSUser* self)
{
    return MI_Instance_Delete(&self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Post(
    _In_ const MSFT_SBSUser* self,
    _In_ MI_Context* context)
{
    return MI_Context_PostInstance(context, &self->__instance);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Set_UserName(
    _Inout_ MSFT_SBSUser* self,
    _In_z_ const MI_Char* str)
{
    return self->__instance.ft->SetElementAt(
        (MI_Instance*)&self->__instance,
        0,
        (MI_Value*)&str,
        MI_STRING,
        0);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_SetPtr_UserName(
    _Inout_ MSFT_SBSUser* self,
    _In_z_ const MI_Char* str)
{
    return self->__instance.ft->SetElementAt(
        (MI_Instance*)&self->__instance,
        0,
        (MI_Value*)&str,
        MI_STRING,
        MI_FLAG_BORROW);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Clear_UserName(
    _Inout_ MSFT_SBSUser* self)
{
    return self->__instance.ft->ClearElementAt(
        (MI_Instance*)&self->__instance,
        0);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Set_PassWord(
    _Inout_ MSFT_SBSUser* self,
    _In_z_ const MI_Char* str)
{
    return self->__instance.ft->SetElementAt(
        (MI_Instance*)&self->__instance,
        1,
        (MI_Value*)&str,
        MI_STRING,
        0);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_SetPtr_PassWord(
    _Inout_ MSFT_SBSUser* self,
    _In_z_ const MI_Char* str)
{
    return self->__instance.ft->SetElementAt(
        (MI_Instance*)&self->__instance,
        1,
        (MI_Value*)&str,
        MI_STRING,
        MI_FLAG_BORROW);
}

MI_INLINE MI_Result MI_CALL MSFT_SBSUser_Clear_PassWord(
    _Inout_ MSFT_SBSUser* self)
{
    return self->__instance.ft->ClearElementAt(
        (MI_Instance*)&self->__instance,
        1);
}

/*
**==============================================================================
**
** MSFT_SBSUser provider function prototypes
**
**==============================================================================
*/

/* The developer may optionally define this structure */
typedef struct _MSFT_SBSUser_Self MSFT_SBSUser_Self;

MI_EXTERN_C void MI_CALL MSFT_SBSUser_Load(
    _Outptr_result_maybenull_ MSFT_SBSUser_Self** self,
    _In_opt_ MI_Module_Self* selfModule,
    _In_ MI_Context* context);

MI_EXTERN_C void MI_CALL MSFT_SBSUser_Unload(
    _In_opt_ MSFT_SBSUser_Self* self,
    _In_ MI_Context* context);

MI_EXTERN_C void MI_CALL MSFT_SBSUser_EnumerateInstances(
    _In_opt_ MSFT_SBSUser_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_opt_ const MI_PropertySet* propertySet,
    _In_ MI_Boolean keysOnly,
    _In_opt_ const MI_Filter* filter);

MI_EXTERN_C void MI_CALL MSFT_SBSUser_GetInstance(
    _In_opt_ MSFT_SBSUser_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_ const MSFT_SBSUser* instanceName,
    _In_opt_ const MI_PropertySet* propertySet);

MI_EXTERN_C void MI_CALL MSFT_SBSUser_CreateInstance(
    _In_opt_ MSFT_SBSUser_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_ const MSFT_SBSUser* newInstance);

MI_EXTERN_C void MI_CALL MSFT_SBSUser_ModifyInstance(
    _In_opt_ MSFT_SBSUser_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_ const MSFT_SBSUser* modifiedInstance,
    _In_opt_ const MI_PropertySet* propertySet);

MI_EXTERN_C void MI_CALL MSFT_SBSUser_DeleteInstance(
    _In_opt_ MSFT_SBSUser_Self* self,
    _In_ MI_Context* context,
    _In_opt_z_ const MI_Char* nameSpace,
    _In_opt_z_ const MI_Char* className,
    _In_ const MSFT_SBSUser* instanceName);


#endif /* _MSFT_SBSUser_h */
