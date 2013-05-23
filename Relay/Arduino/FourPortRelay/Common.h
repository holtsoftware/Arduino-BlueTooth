#ifndef _COMMON_H_
#define _COMMON_H_

#define Interface class

#define DeclareInterface(name) Interface name { \
          public: \
          virtual ~name() {}

#define DeclareBasedInterface(name, base) class name : \
        public base { \
           public: \
           virtual ~name() {}

#define EndInterface };

#endif