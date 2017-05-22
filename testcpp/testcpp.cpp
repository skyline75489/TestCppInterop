// testcpp.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <memory>

#include "testcpp.h"


Delegates* _delegate = nullptr;

// This stands for the actual callback from engine
void OnInternalSuccess(Entry& a) {
  printf("%2d", a.value);
}

void Register(Delegates* delegates)
{
  printf("Registering\n");
  _delegate = new Delegates();
  *_delegate = *delegates;
}

void Start()
{
  printf("Starting\n");
  OnInternalSuccessDelegate a = OnInternalSuccess;
  _delegate->execute_delegate(new ExecutorCallbackImpl(), a);
}