#pragma once

#define EXPORT_API __declspec(dllexport)

struct Entry {
  const char * name;
  int value;
};

struct AEntry : Entry {
  int a;
};

struct BEntry : Entry {
  int b;
};

class ExecutorCallback
{
public:
  virtual void OnSuccess(Entry& a) = 0;
};

typedef void(*OnInternalSuccessDelegate)(Entry& a);
typedef void(CALLBACK *OnSuccessDelegate)(Entry& a, OnInternalSuccessDelegate func);

// This is our implementation
void CALLBACK OnSuccess(Entry& a, OnInternalSuccessDelegate func)
{
  func(a);
  printf("Finished\n");
}

class ExecutorCallbackImpl
{
public:
  ExecutorCallbackImpl() :
    on_success_delegate(nullptr)
  {
    on_success_delegate = OnSuccess;
  }

  OnSuccessDelegate on_success_delegate;
};

typedef void(CALLBACK *ExecuteDelegate)(ExecutorCallbackImpl* callback, OnInternalSuccessDelegate internalSuccess);

class Delegates
{
public:
  Delegates() :
    execute_delegate(nullptr)
  {
  }

  ExecuteDelegate execute_delegate;
};

#ifdef __cplusplus
extern "C"
{
#endif
  EXPORT_API void PrintNumber(int number);
  EXPORT_API int PrintEntry(Entry* entry);
  EXPORT_API void PrintEntry2(Entry* entry);
  EXPORT_API void Register(Delegates* delegates);
  EXPORT_API void Start();
#ifdef __cplusplus
}
#endif

void PrintNumber(int number)
{
  printf("%d\n", number);
}

void WINAPI PrintNumber2(int number)
{
  printf("%d\n", number);
}

int PrintEntry(Entry* entry)
{
  printf("%s : %d\n", entry->name, entry->value);
  return 1;
}

void PrintEntry2(Entry* entry)
{
  printf("%s : %d\n", entry->name, entry->value);
  GlobalFree(entry);
}