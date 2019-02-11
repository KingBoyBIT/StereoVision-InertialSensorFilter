cmake_minimum_required(VERSION 3.13)
project(SOGP C)

set(CMAKE_C_STANDARD 99)

add_executable(SOGP main.c MatrixFunc.c MatrixFunc.h)