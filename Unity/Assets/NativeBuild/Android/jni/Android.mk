APP_ABI := armeabi-v7a arm64-v8a x86 x86_64

LOCAL_PATH := $(call my-dir)
include $(CLEAR_VARS)
LOCAL_MODULE := kcplib
LOCAL_CPPFLAGS += -std=c++11
LOCAL_CPPFLAGS += -fvisibility=hidden
LOCAL_SRC_FILES := ../../src/galois_table.c \
../../src/ikcp.c \
../../src/fec.cpp \
../../src/galois.cpp \
../../src/galois_noasm.cpp \
../../src/inversion_tree.cpp \
../../src/matrix.cpp \
../../src/reedsolomon.cpp \
../../src/sess.cpp \
../../src/wrap.cpp 

include $(BUILD_SHARED_LIBRARY)