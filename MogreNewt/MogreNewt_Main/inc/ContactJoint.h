#pragma once

#include <Newton.h>
#include "ContactMaterial.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

ref class Body;

public value class ContactJoint
{
  NewtonJoint *_joint;

internal:
  ContactJoint(NewtonJoint *joint);

public:
  property Body^ Body0
  {	
    Body^ get();
  }

  property Body^ Body1
  {	
    Body^ get();
  }

  value class ContactEnumerator sealed : System::Collections::Generic::IEnumerator<ContactMaterial>
  {
    const ::NewtonJoint* _joint;
    void* _current;

  internal:
    ContactEnumerator(NewtonJoint *joint) : _joint(joint) {
      Reset();
    }
  public:																				
    ContactEnumerator(ContactJoint joint);

    virtual bool MoveNext();														

    property ContactMaterial Current																
    {																				
      virtual ContactMaterial get();
    }																				
    property Object^ NonGenericCurrent												
    {
    private: virtual Object^ get() sealed = System::Collections::IEnumerator::Current::get
             {																			
               return Current;														
             }																			
    }																				

    virtual void Reset() {
      _current = (void*)-1;
    }
  };


  value class ContactsEnumerable : Collections::Generic::IEnumerable<ContactMaterial>
  {
    NewtonJoint *_joint;

  public:
    ContactsEnumerable(ContactJoint joint);

  private: virtual Collections::IEnumerator^ NonGenericGetEnumerator() sealed = Collections::IEnumerable::GetEnumerator
           {
             return ContactEnumerator(_joint);
           }
  public: virtual Collections::Generic::IEnumerator<ContactMaterial>^ GetEnumerator()
          {
            return ContactEnumerator(_joint);
          }
  };


  property ContactsEnumerable ContactMaterials {
    ContactsEnumerable get() {
      return ContactsEnumerable(*this);
    }
  }

};


}	// end namespace MogreNewt

