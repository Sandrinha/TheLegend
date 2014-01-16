#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <math.h>
#include <time.h>
#include <GL/glut.h>

//PROLOG
#include "stdafx.h"
#include "SWI-cpp.h" 
#include <iostream>  
using namespace std;


int env = _putenv("SWI_HOME_DIR=C:\\Program Files (x86)\\swipl");
char* argv1[] = {"libswipl.dll", "-s", "JogoDoGaloV1.2.pl", NULL};
PlEngine e(3,argv1);	

//FIM DO PROLOG

#ifndef M_PI
#define M_PI 3.1415926535897932384626433832795
#endif

#define RAD(x)          (M_PI*(x)/180)
#define GRAUS(x)        (180*(x)/M_PI)

#define DEBUG               0

/* VARIAVEIS GLOBAIS */

typedef struct {
	GLboolean   doubleBuffer;
	GLint		vista;
	GLint		luz;
}Estado;

Estado estado;

// luzes
GLfloat LightAmbient[]= { 0.5f, 0.5f, 0.5f, 1.0f };
GLfloat LightDiffuse[]= { 0.5f, 0.5f, 0.5f, 1.0f };
GLfloat LightPosition[]= { 5.0f, 25.0f, 5.0f, 1.0f };
GLfloat mat_specular[] = { 1.0, 1.0, 1.0, 1.0 };

// variaveis do rato: Win = tamanho da janela, mouse = posiçao do rato
int mouse_x, mouse_y, Win_x, Win_y, object_select;

// Usado para rotaçao de X's e O's
int spin, spinboxes;

// Win = 1 player wins, -1 computer wins, 2 tie.
// player or computer; 1 = X, -1 = O
// start_game indicates that game is in play.
int player, computer, win, start_game;


// alingment of boxes in which one can win
// We have 8 posiblities, 3 accross, 3 down and 2 diagnally
//
// 0 | 1 | 2
// 3 | 4 | 5
// 6 | 7 | 8
//
// row, colunm, diagnal information

static int box[8][3] = {{0, 1, 2}, {3, 4, 5}, {6, 7, 8}, {0, 3, 6},
                {1, 4, 7}, {2, 5, 8}, {0, 4, 8}, {2, 4, 6}};

// Storage for our game board
// 1 = X's, -1 = O's, 0 = open space

int box_map[9];
// center x,y location for each box
int object_map[9][2] = {{-6,6},{0,6},{6,6},{-6,0},{0,0},{6,0},{-6,-6},{0,-6},{6,-6}};

// quadric pointer for build our X
GLUquadricObj *Cylinder;

// Começa rotina de jogo
void inicia_jogo(void)
{
int i;

// Limpa mapa para novo jogo
for( i = 0; i < 9; i++)
	{
	box_map[i] = 0;
    }

// Sem vencedor coloca win a 0
win = 0;
start_game = 1;
}

// Check for three in a row/colunm/diagnal
// returns 1 if there is a winner
int check_move( void )
{

int i, t = 0;

//Check for three in a row
for( i = 0; i < 8; i++)
	{
   	 t = box_map[box[i][0]] + box_map[box[i][1]] + box_map[box[i][2]];
	 if ( (t == 3) || ( t == -3) )
         {
          spinboxes = i;
          return( 1 );
         }
    }
t = 0;
// check for tie
for( i = 0; i < 8; i++)
	{
   	 t = t + abs(box_map[box[i][0]]) + abs( box_map[box[i][1]]) + abs( box_map[box[i][2]]);
    }

if ( t == 24 ) return( 2 );

return( 0 );
}

// aplica a jogada do computador
int jogada_pc(int m)
{
	
	box_map[m-1] = computer;
	return(1);

}

/* Inicialização do ambiente OPENGL */
void Init(void)
{
	// Variaveis de estado para vista Ortogográfica/Perspectiva e Luz Ligada/Desligada
	estado.vista = 0;
	estado.luz = 0;

   glClearColor (1.0, 1.0, 1.0, 0.0);  // When screen cleared, use black.
   glShadeModel (GL_SMOOTH);  // How the object color will be rendered smooth or flat
   glEnable(GL_DEPTH_TEST);   // Check depth when rendering
   // Lighting is added to scene
   glLightfv(GL_LIGHT1 ,GL_AMBIENT, LightAmbient);
   glLightfv(GL_LIGHT1 ,GL_DIFFUSE, LightDiffuse);
   glLightfv(GL_LIGHT1 ,GL_POSITION, LightPosition);
   glEnable(GL_LIGHTING);  // Turn on lighting
   glEnable(GL_LIGHT1);    // Turn on light 1

   start_game = 0;
   win = 0;

   // Create a new quadric
   Cylinder = gluNewQuadric();
   gluQuadricDrawStyle( Cylinder, GLU_FILL );
   gluQuadricNormals( Cylinder, GLU_SMOOTH );
   gluQuadricOrientation( Cylinder, GLU_OUTSIDE );
}

void play_x()
{
	//select=1;
	player = 1;
	   computer = -1;

	   //PROLOG


		PlTermv av(2);
		av[0] =  PlCompound("x");
 

		PlQuery q("inicio",av);

		q.next_solution();

		//FIM PROLOG 
		inicia_jogo();
}

void play_y()
{
	player = -1;
      computer = 1;

	  //PROLOG

	   

		PlTermv av(2);
		av[0] =  PlCompound("o");
 

		PlQuery q("inicio",av);

		q.next_solution();

		//FIM PROLOG 

	  inicia_jogo();
	  jogada_pc(av[1]);
}

void mouse(int button, int state, int x, int y)
{
// We convert windows mouse coords to out openGL coords
mouse_x =  (18 * (float) ((float)x/(float)Win_x))/6;
mouse_y =  (18 * (float) ((float)y/(float)Win_y))/6;

// What square have they clicked in?
object_select = mouse_x + mouse_y * 3;

if ( start_game == 0)
	{
    if ((button == GLUT_RIGHT_BUTTON) && (state == GLUT_DOWN))
      {
       player = 1;
	   computer = -1;

	   //PROLOG
		PlTermv av(2);
		av[0] =  PlCompound("x");
 

		PlQuery q("inicio",av);

		q.next_solution();
		//FIM PROLOG

	   inicia_jogo();
	   return;
      }

    if ((button == GLUT_LEFT_BUTTON) && (state == GLUT_DOWN))
      {
	  player = -1;
      computer = 1;

	  //PROLOG

		PlTermv av(2);
		av[0] =  PlCompound("o");
 

		PlQuery q("inicio",av);

		q.next_solution();

		//FIM PROLOG 

	  inicia_jogo();
	  jogada_pc(av[1]);
	  return;
      }
   }

if ( start_game == 1)
	{
    if ((button == GLUT_LEFT_BUTTON) && (state == GLUT_DOWN))
      {
      if (win == 0)
         {
         if (box_map[ object_select ] == 0)
            {
            box_map[ object_select ] = player;
	        win = check_move();
	        if (win == 1)
	           {
	           start_game = 0;
               return;
               }
			if ( win == 2 ){
				start_game = 0;
				return;
			}

			//PROLOG	

			PlTermv av(3);

			int p1 = (object_select+1);
		
			if(player == -1)
				av[0] = PlCompound("2");
			else
				av[0] = PlCompound("1");
		
			switch (object_select)
			{
			case 0: av[1] = PlCompound("1");
				break;
			case 1: av[1] = PlCompound("2");
				break;
			case 2: av[1] = PlCompound("3");
				break;
			case 3: av[1] = PlCompound("4");
				break;
			case 4: av[1] = PlCompound("5");
				break;
			case 5: av[1] = PlCompound("6");
				break;
			case 6: av[1] = PlCompound("7");
				break;
			case 7: av[1] = PlCompound("8");
				break;
			case 8: av[1] = PlCompound("9");
				break;
			}

			PlQuery q("jogar",av);

			q.next_solution();

			//FIM PROLOG 

            jogada_pc(av[2]);
	        win = check_move();
	        if (win == 1)
	            {
                win = -1;
                start_game = 0;
                }
            }
         }
	  }
   }

if ( win == 2 )start_game = 0;

}

void options(int id)
{
 switch(id)
 {
  
  case 1:
	  glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT); 
	  play_x();
	  break;
  case 2:
	  glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT);
     play_y();
      break;
  case 3:
	  exit(1);
 }
}

/**************************************
***  callbacks de janela/desenho    ***
**************************************/

// CALLBACK PARA REDIMENSIONAR JANELA

void Reshape(int w, int h)
{
	Win_x = w;
   Win_y = h;
   glViewport (0, 0, (GLsizei) w, (GLsizei) h);
   glMatrixMode (GL_PROJECTION);
   glLoadIdentity ();

}

// ... definicao das rotinas auxiliares de desenho ...

// I use this to put text on the screen
void Sprint( int x, int y, char *st)
{
	int l,i;

	l=strlen( st ); // see how many characters are in text string.
	glRasterPos2i( x, y); // location to start printing text
	for( i=0; i < l; i++)  // loop until i is greater then l
		{
		glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, st[i]); // Print a character on the screen
	}

}

void Draw_O(int x, int y, int z, int a)
{

glPushMatrix();
glTranslatef(x, y, z);
glRotatef(a, 1, 0, 0);
glutSolidTorus(0.5, 2.0, 8, 16);
glPopMatrix();

}


void Draw_X(int x, int y, int z, int a)
{

glPushMatrix();
glTranslatef(x, y, z);
glPushMatrix();
glRotatef(a, 1, 0, 0);
glRotatef(90, 0, 1, 0);
glRotatef(45, 1, 0, 0);
glTranslatef( 0, 0, -3);
gluCylinder( Cylinder, 0.5, 0.5, 6, 16, 16);
//glutSolidCone( 2.5, 3.0, 16, 8 );
glPopMatrix();
glPushMatrix();
glRotatef(a, 1, 0, 0);
glRotatef(90, 0, 1, 0);
glRotatef(315, 1, 0, 0);
glTranslatef( 0, 0, -3);
gluCylinder( Cylinder, 0.5, 0.5, 6, 16, 16);
//glutSolidCone( 2.5, 3.0, 16, 8 );
glPopMatrix();
glPopMatrix();

}

// Callback de desenho

void Draw(void)
{
	int ix, iy;
	int i;
	int j;

	glClear (GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);  //Clear the screen

	glMatrixMode (GL_PROJECTION);  // Tell opengl that we are doing project matrix work
	glLoadIdentity();  // Clear the matrix
	glOrtho(-9.0, 9.0, -9.0, 9.0, 0.0, 30.0);  // Setup an Ortho view
	glMatrixMode(GL_MODELVIEW);  // Tell opengl that we are doing model matrix work. (drawing)
	glLoadIdentity(); // Clear the model matrix

	glDisable(GL_COLOR_MATERIAL);
	glDisable(GL_LIGHTING);
	glColor3f(0.0, 0.0, 1.0);

	if ( start_game == 0 )
	   {
	   Sprint(-2, 0, "Jogo do Galo");
	   Sprint(-7, -2, "Para comecar clique no botao esquerdo");
	   }

	if (win == 1) Sprint( -3, 2, "Voce ganhou");
	if (win == -1) Sprint( -4, 2, "O Computador ganhou");
	if (win == 2) Sprint( -2, 2, "Empate");

	// Setup view, and print view state on screen
	if (estado.vista == 1)
		{
		glColor3f( 0.0, 0.0, 1.0);
		Sprint(-3, 8, "Vista Perspectiva");
		glMatrixMode (GL_PROJECTION);
		glLoadIdentity();
		gluPerspective(60, 1, 1, 30);
		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();
		}else
		{
		glColor3f( 0.0, 0.0, 1.0);
		Sprint(-2, 8, "Vista Ortografica");
		}

	// Lighting on/off
		if (estado.luz == 1)
		{
		glDisable(GL_LIGHTING);
		glDisable(GL_COLOR_MATERIAL);
		}else
		{
		glEnable(GL_LIGHTING);
		glEnable(GL_COLOR_MATERIAL);
		}


	gluLookAt( 0, 0, 20, 0, 0, 0, 0, 1, 0);


	// Draw Grid
	for( ix = 0; ix < 4; ix++)
		{
		   glPushMatrix();
		   glColor3f(1,1,1);
		   glBegin(GL_LINES);
		   glVertex2i(-9 , -9 + ix * 6);
		   glVertex2i(9 , -9 + ix * 6 );
		   glEnd();
		   glPopMatrix();
		}
		for( iy = 0; iy < 4; iy++ )
		   {
		   glPushMatrix();
		   glColor3f(1,1,1);
		   glBegin(GL_LINES);
		   glVertex2i(-9 + iy * 6, 9 );
		   glVertex2i(-9 + iy * 6, -9 );
		   glEnd();
		   glPopMatrix();
		   }

	glColorMaterial(GL_FRONT, GL_AMBIENT);
	glColor4f(0.5, 0.5, 0.5, 1.0);
	glColorMaterial(GL_FRONT, GL_EMISSION);
	glColor4f(0.0, 0.0, 0.0, 1.0 );
	glColorMaterial(GL_FRONT, GL_SPECULAR);
	glColor4f(0.35, 0.35, 0.35, 1.0);
	glColorMaterial(GL_FRONT, GL_DIFFUSE);
	glColor4f(0.69, 0.69, 0.69, 1.0);
	//glDisable(GL_COLOR_MATERIAL);
	glColor3f( 1.0, 0.0, 1.0);  // Cube color
	//glEnable(GL_COLOR_MATERIAL);
	// Draw object in box's

	for( i = 0; i < 9; i++)
	   {
	   j = 0;
	   if (abs( win ) == 1 )
		  {
		  if ( (i == box[spinboxes][0]) || (i == box[spinboxes][1]) || (i == box[spinboxes][2]))
			 {
			  j = spin;
			  }else j = 0;
			}
	   if(box_map[i] == 1) Draw_X( object_map[i][0], object_map[i][1], -1, j);

	   if(box_map[i] == -1) Draw_O( object_map[i][0], object_map[i][1], -1, j);
	   }

	//glDisable(GL_COLOR_MATERIAL);

	glutSwapBuffers();
}

/*******************************
***   callbacks timer/idle   ***
*******************************/

// This creates the spinning of the cube.
static void TimeEvent(int te)
{

    spin++;  // increase cube rotation by 1
	if (spin > 360) spin = 0; // if over 360 degress, start back at zero.
	glutPostRedisplay();  // Update screen with new rotation data
	glutTimerFunc( 10, TimeEvent, 1);  // Reset our timmer.
}

void imprime_ajuda(void)
{
	printf("\n\nJogo do Galo\n");
	printf("h,H - Ajuda \n");
	printf("v,V - Alterna entre vistas\n");
	printf("l,L - Alterna luzes\n");
	printf("ESC - Sair\n");
}

/*******************************
***  callbacks de teclado    ***
*******************************/

// Callback para interaccao via teclado (carregar na tecla)

void Key(unsigned char key, int x, int y)
{
	switch (key) {
	case 27:
		exit(1);
		// ... accoes sobre outras teclas ... 

	case 'h':
	case 'H':
		imprime_ajuda();
		break;
	case 'v':
	case 'V':
		estado.vista = abs(estado.vista -1);
		break;
	case 'l':
	case 'L':
		  estado.luz = abs(estado.luz -1);
		  break;

	}

}

void main(int argc, char **argv)
{
	estado.doubleBuffer = GL_TRUE;

	glutInit(&argc, argv);
	glutInitWindowPosition(0, 0);
	glutInitWindowSize(500, 500);
	glutInitDisplayMode(((estado.doubleBuffer) ? GLUT_DOUBLE : GLUT_SINGLE) | GLUT_RGB);
	if (glutCreateWindow("Jogo do Galo") == GL_FALSE)
		exit(1);

	Init();

	imprime_ajuda();

	glutCreateMenu(options);
	glutAddMenuEntry("Jogar com o X",1);
	glutAddMenuEntry("Jogar com o O",2);
	glutAddMenuEntry("Sair",3);
	glutAttachMenu(GLUT_RIGHT_BUTTON);

	// Registar callbacks do GLUT

	// callbacks de janelas/desenho
	glutReshapeFunc(Reshape);
	glutDisplayFunc(Draw);

	// Callbacks de teclado
	glutKeyboardFunc(Key);


	glutMouseFunc(mouse);
	glutTimerFunc( 10, TimeEvent, 1);

	// COMECAR...
	glutMainLoop();

}