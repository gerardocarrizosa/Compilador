﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public class Sintactico
    {
        public List<Error> listaErrorSintactico;
        public List<Token> listaTokens;
        private int[] listaSintactico = new int[100];
        public bool error = false;
        public bool revision = false;

        public int punteroLexico = 0;
        int punteroSintactico = 1;
        int intentosRecuperar;
        TipoRecuperacion tipoRecuperacion;

        enum TipoRecuperacion
        {
            Ninguna,
            Falta,
            Sobra,
            Diferentes,
            Urgencia,
            NoMas
        }

        public int[,] MatrizTransicionSintactico = new int[,]
        {
            //      id |numE|numD|cade|bool|  + |  - |  * |  / |  > |  < | <= | >= | == | != | && | || | !! | ++ | -- |  ( |  ) |22{ |  } |  [ |  ] |  , |  ; |28: |  ? |  = |  . |  ' |vaci|cade| ent|dobl|bool|clas| si |impo|sino|para|mien| haz|nulo|verd|fals|camb|caso|romp| var|impr|defa|leer|  λ
          /*s 0 */{-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,  1 ,-600,  1 ,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600},
   /*programa 1 */{-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,  2 ,-600,  2 ,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600},
  /*librerías 2 */{-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,  3 ,-600,  4 ,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600,-600},
   /*librería 3 */{-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,  5 ,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601,-601},
     /*clases 4 */{-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,  9 ,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604},
    /*clases1 5 */{-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,  7 ,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,  9 ,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,  7 },
      /*clase 6 */{-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,  9 ,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604,-604},
   /*miembros 7 */{ 11 ,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603, 11 , 10 ,-603,-603,-603,-603,-603,-603,-603,-603,-603, 11 , 11 , 11 , 11 , 11 ,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603, 11 },
    /*miembro 8 */{ 12 ,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603, 31 , 12 , 12 , 12 , 12 ,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603,-603, 12 },
/*declaracion 9 */{ 13 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 13 ,-605,-605,-605, 13 , 13 , 13 , 13 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-603, 13 },
  /*multiples 10*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 15 , 14 ,-605,-605, 14 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
  /*expresion 11*/{ 16 , 16 , 16 , 16 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 16 , 16 ,-605,-605, 16 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 94 , 95 ,-605,-605,-605,-605,-605,-605, 91 ,-605},
     /*factor 12*/{ 17 , 18 , 19 , 20 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 92 , 93 ,-605,-605,-605,-605,-605,-605,-605,-605},
    /*termino 13*/{ 22 , 22 , 22 , 22 ,-605, 22 , 22 , 22 , 22 , 21 , 21 , 21 , 21 , 21 , 21 , 21 , 21 , 21 ,-605,-605,-605, 22 , 22 ,-605,-605,-605,-605, 21 , 21 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 21 },
     /*opIter 14*/{ 87 , 87 , 87 , 87 ,-605, 23 , 24 , 25 , 26 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 87 , 87 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
       /*tipo 15*/{ 82 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 15 ,-605,-605,-605,-605,-605,-605, 27 , 28 , 29 , 30 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 81 },
    /*funcion 16*/{-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606, 32 ,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-605,-606},
 /*parametros 17*/{-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606, 33 ,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606, 34 , 34 , 34 , 34 ,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-605,-606},
/*parametros1 18*/{-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606, 36 ,-606,-606,-606,-606, 35 ,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-605,-606},
 /*sentencias 19*/{ 38 ,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606, 37 ,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606, 38 , 38 , 38 , 38 ,-606, 38 ,-606,-606, 38 , 38 , 38 ,-606,-606,-606, 38 ,-606,-606,-606, 38 ,-606,-605, 36 },
  /*sentencia 20*/{ 39 ,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606,-606, 39 , 39 , 39 , 39 ,-606, 40 ,-606,-606, 41 , 42 , 43 ,-606,-606,-606, 44 ,-606,-606,-606, 45 ,-606,-605,-606},
         /*if 21*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 46 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
/*condicional 22*/{ 47 , 47 , 47 , 47 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 47 ,-605,-605,-605, 47 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 47 , 47 ,-605,-605,-605,-605,-605,-605,-605,-605},
 /*factorCond 23*/{ 50 , 50 , 50 , 50 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 50 ,-605,-605,-605, 50 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 48 , 49 ,-605,-605,-605,-605,-605,-605,-605,-605},
   /*termCond 24*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 52 , 52 , 52 ,-605,-605,-605, 51 ,-605,-605,-605,-605,-605, 51 , 51 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 51 },
      /*opRel 25*/{-605,-605,-605,-605,-605,-605,-605,-605,-605, 53 , 54 , 56 , 55 , 57 , 58 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
      /*opLog 26*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 59 , 60 , 61 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
       /*else 27*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 62 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 65 ,-605, 63 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 62 ,-605,-605, 62 },
      /*else1 28*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 64 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 65 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
        /*for 29*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 66 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
/*valorInicia 30*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 67 , 67 , 67 , 67 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
 /*incremento 31*/{ 68 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
/*incrementos 32*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 69 , 70 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
      /*while 33*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 71 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
         /*do 34*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 72 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605},
     /*switch 35*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 73 ,-605,-605,-605,-605,-605,-605,-605},
      /*cases 36*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 75 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 74 ,-605,-605,-605,-605,-605,-605},
       /*case 37*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 76 ,-605,-605,-605,-605,-605,-605},
    /*default 38*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 78 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 77 ,-605,-605},
   /*escribir 39*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 79 ,-605,-605,-605},
 /*asignacion 40*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 81 ,-605,-605, 80 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 89 ,-605},
   /*simbolos 41*/{ 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 , 88 ,-605,-605, 84 , 85 ,-605,-605, 86 , 87 ,-605, 88 , 88 ,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 88 , 88 ,-605,-605,-605,-605,-605,-605,-605,-605},
       /*leer 42*/{-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605,-605, 90 ,-605},
        };

        public int[,] RepositorioReglas = new int[,]
        {
            /*1 s*/           { 1001 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*2 programa*/    { 1004 , 1002 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*3 librerías*/   {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*4 librerías*/   { 1002 , 1003 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*5 librería*/    {  -28 ,   -4 ,  -41 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*6 clases*/      { 1005 , 1006 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*7 clases1*/     {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*8 clases1*/     { 1005 , 1006 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*9 clase*/       {  -24 , 1007 ,  -23 ,   -1 ,  -39 ,  -98 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*10 miembros*/   {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*11 miembros*/   { 1007 , 1008 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*12 miembro*/    { 1009 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*13 declaracion*/{  -28 , 1040 , 1010 ,   -1 , 1015 ,  -98 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*14 multiples*/  {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*15 multiples*/  { 1010 ,   -1 ,  -27 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*16 expresion*/  { 1013 , 1041 , 1012 , 1041 ,  -98 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*17 factor*/     {   -1 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*18 factor*/     {   -2 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*19 factor*/     {   -3 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*20 factor*/     {   -4 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*21 termino*/    {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*22 termino*/    { 1011 , 1014 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*23 opIter*/     {   -6 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*24 opIter*/     {   -7 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*25 opIter*/     {   -8 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*26 opIter*/     {   -9 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*27 tipo*/       {  -35 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*28 tipo*/       {  -36 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*29 tipo*/       {  -37 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*30 tipo*/       {  -38 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*31 miembro*/    { 1016 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*32 funcion*/    {  -24 , 1019 ,  -23 ,  -22 , 1017 ,  -21 ,   -1 ,  -34 ,  -98 ,   0  ,   0 ,   0 ,   0 },//
            /*33 parámetros*/ {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*34 parámetros*/ { 1018 ,   -1 , 1015 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*35 parámetros1*/{ 1018 ,   -1 , 1015 ,  -27 ,  -98 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*36 parámetros1*/{  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*37 sentencias*/ {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*38 sentencias*/ { 1019 , 1020 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*39 sentencia*/  { 1009 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*40 sentencia*/  { 1021 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*41 sentencia*/  { 1029 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*42 sentencia*/  { 1033 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*43 sentencia*/  { 1034 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*44 sentencia*/  { 1035 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*45 sentencia*/  { 1039 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*46 if*/         { 1027 ,  -24 , 1019 ,  -23 ,  -29 , 1022 ,  -40 ,  -98 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*47 condicional*/{ 1024 , 1023 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*48 factorCond*/ {  -47 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*49 factorCond*/ {  -48 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*50 factorCond*/ { 1011 , 1025 , 1011 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*51 termCond*/   {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*52 termCond*/   { 1022 , 1026 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*53 opRel*/      {  -10 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*54 opRel*/      {  -11 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*55 opRel*/      {  -13 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*56 opRel*/      {  -12 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*57 opRel*/      {  -14 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*58 opRel*/      {  -15 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*59 opLog*/      {  -16 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*60 opLog*/      {  -17 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*61 opLog*/      {  -18 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*62 else*/       {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*63 else*/       { 1028 ,  -42 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*64 else1*/      {  -24 , 1019 ,  -23 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*65 else1*/      { 1021 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*66 for*/        {  -24 , 1019 ,  -23 ,  -22 , 1031 ,  -28 , 1022 , 1030 ,  -21 , -43 , -98  ,   0 ,   0 },//
            /*67 valorInicia*/{ 1009 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*68 incremento*/ { 1032 ,   -1 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*69 incrementos*/{  -19 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*70 incrementos*/{  -20 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*71 while*/      {  -28 ,  -24 , 1019 ,  -23 ,  -22 , 1022 ,  -21 ,  -44 ,  -98 ,   0  ,   0 ,   0 ,   0 },//
            /*72 do*/         {  -29 , 1022 ,  -44 ,  -24 , 1019 ,  -23 ,  -45 ,  -98 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*73 switch*/     {  -24 , 1039 , 1037 ,  -23 ,  -22 , 1011 ,  -21 ,  -49 ,  -98 ,   0  ,   0 ,   0 ,   0 },//
            /*74 cases*/      { 1037 , 1038 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*75 cases*/      {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*76 case*/       {  -28 ,  -51 , 1019 ,  -29 , 1012 ,  -50 ,  -98 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*77 default*/    {  -28 ,  -51 , 1019 ,  -29 ,  -54 ,  -98 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*78 default*/    {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*79 escribir*/   {  -28 ,  -22 , 1012 ,  -21 ,  -53 ,  -98 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*80 asignacion*/ { 1011 ,  -31 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*81 asignacion*/ {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*82 tipo*/       {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*83 opIter*/     {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*84 simbolos*/   {  -21 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*85 simbolos*/   {  -22 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*86 simbolos*/   {  -25 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*87 simbolos*/   {  -26 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*88 simbolos*/   {  -56 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*89 asignacion*/ { 1042 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*90 leer*/       {  -22 ,  -21 ,  -55 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*91 expresion*/  { 1042 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*92 factor*/     {  -47 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*93 factor*/     {  -48 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*92 expresión*/  {  -47 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
            /*93 expresión*/  {  -48 ,  -98 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,    0 ,   0  ,   0 ,   0 ,   0 },//
        };

        public Sintactico(List<Token> listaTokenLexico)
        {
            listaErrorSintactico = new List<Error>();

            listaTokens = listaTokenLexico;
            listaTokens.Add(new Token() { Lexema = "$", Linea = 0, TipoToken = TipoToken.Cadena, ValorToken = -99 });

            listaSintactico[0] = -99;
            listaSintactico[1] = 1000;

            intentosRecuperar = 0;
            tipoRecuperacion = TipoRecuperacion.Ninguna;
        }

        public List<Token> EjecutarSintactico(List<Token> listaTokens)
        {
            int renglon, columna = 0;
            int regla;

            do
            {
                if (listaTokens.Count == punteroLexico)
                {
                    punteroLexico = listaTokens.Count - 1;
                }

                if (listaSintactico[punteroSintactico] < 0)
                {
                    if (listaSintactico[punteroSintactico] == -56)
                    {
                        listaSintactico[punteroSintactico] = 0;
                        punteroSintactico--;
                    }
                    else if (listaSintactico[punteroSintactico] == listaTokens[punteroLexico].ValorToken)
                    {
                        if (listaSintactico[punteroSintactico] == -99)
                        {
                            revision = true;
                            /*if (error)
                                MessageBox.Show("Error en análisis sintáctico");
                            else
                                //MessageBox.Show("Código compilado correctamente");
                            break;*/
                        }
                        else
                        {
                            listaSintactico[punteroSintactico] = 0;
                            punteroLexico++;
                            punteroSintactico--;
                            VerificarRecuperacion();
                        }
                    }
                    else
                    {
                        NuevoErrorr(listaTokens, ref revision, 1);
                    }
                }
                else
                {
                    renglon = BuscarRenglon(listaSintactico[punteroSintactico]);
                    columna = BuscarColumna(listaTokens[punteroLexico].ValorToken);

                    regla = MatrizTransicionSintactico[renglon, columna];

                    if (regla > 0)
                    {
                        InsertarRegla(regla);
                        VerificarRecuperacion();
                    }
                    else
                    {
                        
                        if (intentosRecuperar < 1)
                        {
                            tipoRecuperacion = TipoRecuperacion.Sobra;
                        }
                        NuevoErrorr(listaTokens, ref revision, regla);
                    }
                }
            } while (revision != true);
            return listaTokens;
        }

        private Error ManejoDeErrores(int error, int linea)
        {
            string mensajeError = "";
            switch (error)
            {
                case 1:
                    mensajeError = "Se esperaba el simbolo: " + listaSintactico[punteroSintactico];
                    break;
                case -600:
                    mensajeError = "Se esperaba una estructura de clase o librería.";
                    break;
                case -601:
                    mensajeError = "Se esperaba una estructura de librería.";
                    break;
                case -603:
                    mensajeError = "Se esperaba un miembro de una clase.";
                    break;
                case -604:
                    mensajeError = "Se esperaba una estructura de clase";
                    break;
                case -605:
                    mensajeError = "Error de declaración";
                    break;
                case -606:
                    mensajeError = "Se esperaba una estructura de función";
                    break;
                default:
                    break;
            }
            return new Error() { Codigo = error, MensajeError = mensajeError, Tipo = tipoError.Sintactico, Linea = linea - 1};
        }

        private int BuscarColumna(int token)
        {
            switch (token)
            {
                case -1: /*id*/
                    return 0;
                case -2: /*nument*/
                    return 1;
                case -3: /*numdec*/
                    return 2;
                case -4: /*cadenas*/
                    return 3;
                case -5: /*booleano*/
                    return 4;
                case -6: /*+*/
                    return 5;
                case -7: /*-*/
                    return 6;
                case -8: /***/
                    return 7;
                case -9: /*/*/
                    return 8;
                case -10: /*>*/
                    return 9;
                case -11: /*<*/
                    return 10;
                case -12: /*<=*/
                    return 11;
                case -13: /*>=*/
                    return 12;
                case -14: /*==*/
                    return 13;
                case -15: /*!=*/
                    return 14;
                case -16: /*&&*/
                    return 15;
                case -17: /*||*/
                    return 16;
                case -18: /*!!*/
                    return 17;
                case -19: /*++*/
                    return 18;
                case -20: /*--*/
                    return 19;
                case -21: /*(*/
                    return 20;
                case -22: /*)*/
                    return 21;
                case -23: /*{*/
                    return 22;
                case -24: /*}*/
                    return 23;
                case -25: /*[*/
                    return 24;
                case -26: /*]*/
                    return 25;
                case -27: /*,*/
                    return 26;
                case -28: /*;*/
                    return 27;
                case -29: /*:*/
                    return 28;
                case -30: /*?*/
                    return 29;
                case -31: /*=*/
                    return 30;
                case -32: /*.*/
                    return 31;
                case -33: /*'*/
                    return 32;
                case -34: /*void*/
                    return 33;
                case -35: /*cadena*/
                    return 34;
                case -36: /*ent*/
                    return 35;
                case -37: /*doble*/
                    return 36;
                case -38: /*bool*/
                    return 37;
                case -39: /*clase*/
                    return 38;
                case -40: /*si*/
                    return 39;
                case -41: /*importar*/
                    return 40;
                case -42: /*sino*/
                    return 41;
                case -43: /*para*/
                    return 42;
                case -44: /*mientras*/
                    return 43;
                case -45: /*haz*/
                    return 44;
                case -46: /*nulo*/
                    return 45;
                case -47: /*verdadero*/
                    return 46;
                case -48: /*false*/
                    return 47;
                case -49: /*cambiar*/
                    return 48;
                case -50: /*caso*/
                    return 49;
                case -51: /*romper*/
                    return 50;
                case -52: /*var*/
                    return 51;
                case -53: /*imprimir*/
                    return 52;
                case -54: /*default*/
                    return 53;
                case -55: /*leer*/
                    return 54;
                case -56: /*λ*/
                    return 55;
                case -99: /*λ*/
                    return 55;

                default: ////////////CORREGIR ESTO
                    throw new Exception("Error de lógica");
            }
        }

        private int BuscarRenglon(int regla)
        {
            switch (regla)
            {
                case 1000: /* s */
                    return 0;

                case 1001: /* programa */
                    return 1;

                case 1002: /* librerías */
                    return 2;

                case 1003: /* librería */
                    return 3;

                case 1004: /* clases */
                    return 4;

                case 1005: /* clases1 */
                    return 5;

                case 1006: /* clase */
                    return 6;

                case 1007: /* miembros */
                    return 7;

                case 1008: /* miembro */
                    return 8;

                case 1009: /* declaracion */
                    return 9;

                case 1010: /* multiples */
                    return 10;

                case 1011: /* expresion */
                    return 11;

                case 1012: /* factor */
                    return 12;

                case 1013: /* termino */
                    return 13;

                case 1014: /* opIter*/
                    return 14;

                case 1015: /* tipo */
                    return 15;

                case 1016: /* funcion */
                    return 16;

                case 1017: /* parametros */
                    return 17;

                case 1018: /* parametros1 */
                    return 18;

                case 1019: /* sentencias */
                    return 19;

                case 1020: /* sentencia */
                    return 20;

                case 1021: /* if */
                    return 21;

                case 1022: /* condicional */
                    return 22;

                case 1023: /* factorCond */
                    return 23;

                case 1024: /* termCond */
                    return 24;

                case 1025: /* opRel */
                    return 25;

                case 1026: /* opLog */
                    return 26;

                case 1027: /* else */
                    return 27;

                case 1028: /* else1 */
                    return 28;

                case 1029: /* for */
                    return 29;

                case 1030: /* valorInicial */
                    return 30;

                case 1031: /* incremento */
                    return 31;

                case 1032: /* incrementos */
                    return 32;

                case 1033: /* while */
                    return 33;

                case 1034: /* do */
                    return 34;

                case 1035: /* switch */
                    return 35;

                case 1036: /* cases */
                    return 36;

                case 1037: /* case */
                    return 37;

                case 1038: /* default */
                    return 38;

                case 1039: /* escribir */
                    return 39;

                case 1040: /* asignación */
                    return 40;

                case 1041: /* simbolos */
                    return 41;
                case 1042: /* leer */
                    return 42;
                default:
                    throw new Exception("Error de lógica");
            }
        }

        private void InsertarRegla(int regla)
        {
            int i = 0;
            do
            {
                listaSintactico[punteroSintactico] = RepositorioReglas[regla - 1, i];
                punteroSintactico++;
                i++;
            } while (RepositorioReglas[regla - 1, i] != -98);
            punteroSintactico--;
        }

        private void VerificarRecuperacion()
        {
            if (tipoRecuperacion != TipoRecuperacion.Ninguna)
            {
                tipoRecuperacion = TipoRecuperacion.Ninguna;
                intentosRecuperar = 0;
            }
        }

        private void NuevoErrorr(List<Token> listaTokens, ref bool revision, int tipo)
        {
            error = true;
            var nuevoError = ManejoDeErrores(tipo, listaTokens[punteroLexico].Linea);
            listaErrorSintactico.Add(nuevoError);
            revision = true;
        }

    }
}