/*
 * ServerPetraController.c
 * This file is part of <library name>
 *
 * Copyright (C) 2017 - Capitano Jonathan
 *
 * <library name> is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * <library name> is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with <library name>. If not, see <http://www.gnu.org/licenses/>.
 */

// Compilation sous QNX: gcc ./ServerPetraController.c -l socket

#include <stdlib.h>
#include <stdio.h>
#include <fcntl.h>
#include <time.h>
#include <sched.h>
#include <termios.h>
#include <fcntl.h> 
#include <string.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <pthread.h>
#include <errno.h>


#define PORT 7000

struct ACTUATEURS
{
    unsigned CP : 2;
    unsigned C1 : 1;
    unsigned C2 : 1;
    unsigned PV : 1;
    unsigned PA : 1;
    unsigned AA : 1;
    unsigned GA : 1;
};
union
{
	struct ACTUATEURS act;
	unsigned char byte;
} u_act;

struct CAPTEURS
{
    unsigned L1 : 1;
    unsigned L2 : 1;
    unsigned T  : 1;
    unsigned S  : 1;
    unsigned CS : 1;
    unsigned AP : 1;
    unsigned PP : 1;
    unsigned DE : 1;
};
union
{
	struct CAPTEURS capt;
	unsigned char byte;
} u_capt;

int fd_petra_in, fd_petra_out;
int sockS;
int ReadEnable = 0;
pthread_mutex_t MutexReading;

pthread_t pidSendCapteurs;

int Sockette(int* soc, int port);
int Waiting(int* soc, int* conn_desc, struct sockaddr_in* c_addr ,char* buffer);
void* Serveur(void* arg);
void *TraitementClient(void *arg);

int main(int argc, char **argv)
{
    pthread_mutex_init(&MutexReading, NULL);
    //Actuateur
    fd_petra_out = open("/dev/actuateursPETRA", O_WRONLY);
	if(fd_petra_out == -1)
	{
		perror("\n MAIN: PETRA_OUT failed!");
		return 1;
	}
    //capteurs
	fd_petra_in = open("/dev/capteursPETRA", O_RDONLY);
	if ( fd_petra_in == -1 )
	{
		perror("\n MAIN: PETRA_IN failed!");
		return 1;
	}
	
	Serveur(NULL);
    
}


int Sockette(int* soc, int port)
{
	struct sockaddr_in s_addr;
	int yes = 1;
	if((*soc = socket(AF_INET, SOCK_STREAM, 0)) == -1) // Crée la socket TCP
		return -1; 
	
	bzero((char *)&s_addr, sizeof(s_addr)); // Initialise la variable s_addr
	 
	s_addr.sin_family = AF_INET; // Toutes les cartes réseaux peuvent intercepter les paquets
	s_addr.sin_addr.s_addr = htonl(INADDR_ANY); // Convertit l'adresse en Big Endian
	s_addr.sin_port = htons(port); // Convertit le port en Big Endian
	
	setsockopt(*soc, SOL_SOCKET, SO_REUSEADDR, &yes, sizeof(int)); // Paramétrer la socket de manière à ce qu'elle soit réutilisable
	
	if(bind(*soc, (struct sockaddr *)&s_addr, sizeof(struct sockaddr_in)) == -1) // Lier l'adresse au socket
               return -1;
               
	printf("Socket TCP accepté: En écoute !\n");
	return 1;
}


int Waiting(int* soc, int* conn_desc, struct sockaddr_in* c_addr ,char* buffer)
{

	// int size = sizeof(*c_addr);
	socklen_t size = sizeof(*c_addr);
	int taille_lu = 0;
	
	listen(*soc, 1); // Ecoute les connexions sur la socket
		
	if((*conn_desc = accept(*soc, (struct sockaddr *)c_addr, &size)) == -1) // Accepte une connexion sur la socket
	{
		printf ("err. accept errno: %d \n",errno);
		return -1;
	}else{
		printf("socket ok desc: %d",*conn_desc);
	}
	return 1;
}


void* Serveur(void* arg)
{
	
	if(Sockette(&sockS, PORT) == -1)
	{
		perror("Err. de bind");
		exit(-1);
	}
	
	
	pthread_t pidTraitement[2];
	int i;
	
	for(i = 0; i < 2; i+=1)
		pthread_create(&pidTraitement[i], NULL, TraitementClient, NULL);
	
	for(i = 0; i < 2; i+=1)
		pthread_join(pidTraitement[i], NULL);
		
	
	close(sockS);
	
	printf("Le serveur a été fermé !\n");
	pthread_exit(NULL);
	return NULL;
}

void* SendCapteurs(void* arg)
{
    struct timespec tim;
    int* conn_desc = (int*)arg;
    int launched = 1;
	tim.tv_sec = 0;

	tim.tv_nsec = 5000;
	
    
    while(launched)
	{ 
	    
		read(fd_petra_in, &u_capt.byte, 1);
		/*u_capt.capt.L1 = 1;//essai -> Simule les valeurs retournées par le petra
		u_capt.capt.L2 = 1;
		u_capt.capt.T = 1;
		u_capt.capt.S = 1;
		u_capt.capt.CS = 1;
		u_capt.capt.AP = 1;
		u_capt.capt.PP = 1;
		u_capt.capt.DE = 1;*/
		char byte[8] ;
		byte [0] = u_capt.capt.L1;
		byte [1] = u_capt.capt.L2;
		byte [2] = u_capt.capt.T;
		byte [3] = u_capt.capt.S;
		byte [4] = u_capt.capt.CS;
		byte [5] = u_capt.capt.AP;
		byte [6] = u_capt.capt.PP;
		byte [7] = u_capt.capt.DE;
		if(send(*conn_desc,byte/*&u_capt.byte*/,8/*sizeof(u_capt.byte)*/,0) == -1)
		{
			printf("Erreur sur le send de la socket\n");
			/*close(*conn_desc);
			close(sockS);
			exit(1);*/
		}
		
		pthread_mutex_lock(&MutexReading);
		if(!ReadEnable)
        {
            launched = 0;
        }
        pthread_mutex_unlock(&MutexReading);
		nanosleep(&tim,NULL);
	}
}

void *TraitementClient(void *arg)
{
	struct sockaddr_in c_addr;
	
	
	char msgClient[150];
	int tailleMsgRecu = 0;
	int nbreBytesRecus, fin;
	printf("Thread de traitements lancé !\n");
	
	u_act.byte = 0x00;
	u_capt.byte = 0x00;
	
	int conn_desc = 0;

	socklen_t size = sizeof(c_addr);
	int taille_lu = 0;
    
    do
    {
        accept:;
	    listen(sockS, 1); // Ecoute les connexions sur la socket
	
	    if((conn_desc = accept(sockS, (struct sockaddr *)&c_addr, &size)) == -1) // Accepte une connexion sur la socket
	    {
		    printf ("err. accept errno: %d \n",errno);
		    //return -1;
	    }
	    else
	    {
		    printf("socket ok desc: %d\n",conn_desc);
	    }
	    
	    if(ReadEnable == 1)
	    {
	        printf("Quelqu'un s'occupe deja du Petra!\n");
	        close(conn_desc);
	        char byte[8] ;
		    byte [0] = 0;
		    byte [1] = 0;
		    byte [2] = 0;
		    byte [3] = 0;
		    byte [4] = 0;
		    byte [5] = 0;
		    byte [6] = 0;
		    byte [7] = 0;
		
		    if(send(conn_desc,byte,8,0) == -1)
		    {
			    printf("Erreur sur le send de la socket\n");
			    close(conn_desc); /* Fermeture de la socket */
			    //close(sockS); /* Fermeture de la socket */
			    //exit(1);
		    }
	        goto accept;
	    }
	    else
	    {
	        char byte[8] ;
		    byte [0] = u_act.act.C1;
		    byte [1] = u_act.act.C2;
		    byte [2] = u_act.act.PA;
		    byte [3] = u_act.act.PV;
		    byte [4] = u_act.act.AA;
		    byte [5] = u_act.act.GA;
		    byte [6] = u_act.act.CP;
		    byte [7] = 1;
		
		    if(send(conn_desc,byte,8,0) == -1)
		    {
			    printf("Erreur sur le send de la socket\n");
			    close(conn_desc); /* Fermeture de la socket */
			    close(sockS); /* Fermeture de la socket */
			    exit(1);
		    }
	    }
	   
        
		
	    while(1)
	    {
	        char* commande = NULL;
		    char buffer[50];

		
		    if((nbreBytesRecus = recv(conn_desc,buffer,sizeof(buffer),0)) == -1)
		    {
			    printf("Erreur recv nbreBytesRecus: %d errno: %d\n",nbreBytesRecus,errno);
			    printf("Message reçu: %s\n",buffer);
			    //close(sockS);
		    }
		    
		    
		    strtok_r(buffer, ":", &commande);
		
		    printf("commande = %s, buffer = %s\n", commande, buffer);
		
		    if(strcmp("Lecture",buffer) == 0)
		    {
			    printf("Lecture des capteurs\n");
			    
	            ReadEnable = 1;
	            
	            pthread_create(&pidSendCapteurs, NULL, SendCapteurs, &conn_desc);
			   
		    }
		    else if(strcmp("Ecriture",buffer) == 0)
		    {
			    printf("Ecriture sur les Actuateurs\n");
				
			    //read ( fd_petra_out , &u_act.byte , 1 );
			
			    if(strcmp("CONV1_ON",commande)==0)
			    {
				    u_act.act.C1 = 1;
				    printf("C1 ON \n");
			    }
			
			    if(strcmp("CONV1_OFF",commande)==0)
			    {
				    u_act.act.C1 = 0;
				    printf("C1 OFF \n");
			    }
			    if(strcmp("CONV2_ON",commande)==0)
			    {
				    u_act.act.C2 = 1;
				    printf("C2 ON \n");
			    }
			
			    if(strcmp("CONV2_OFF",commande)==0)
			    {
				    u_act.act.C2 = 0;
				    printf("C2 OFF \n");
			    }
			
			    if(strcmp("B_VENT_ON",commande)==0)
			    {
				    u_act.act.PA = 1;
				    printf("PA ON \n");
			    }
			
			    if(strcmp("B_VENT_OFF",commande)==0)
			    {
				    u_act.act.PA = 0;
				    printf("PA OFF \n");
			    }
			
			    if(strcmp("VENT_ON",commande)==0)
			    {
				    u_act.act.PV = 1;
				    printf("PV ON \n");
			    }
			
			    if(strcmp("VENT_OFF",commande)==0)
			    {
				    u_act.act.PV = 0;
				    printf("PV OFF \n");
			    }
			
			    if(strcmp("B_TAP_ON",commande)==0)
			    {
				    u_act.act.AA = 1;
				    printf("AA ON \n");
			    }
			
			    if(strcmp("B_TAP_OFF",commande)==0)
			    {
				    u_act.act.AA = 0;
				    printf("AA OFF \n");
			    }
			
			    if(strcmp("PIN_B_TAP_ON",commande)==0)
			    {
				    u_act.act.GA = 1;
				    printf("GA ON \n");
			    }
			
			    if(strcmp("PIN_B_TAP_OFF",commande)==0)
			    {
				    u_act.act.GA = 0;
				    printf("GA OFF \n");
			    }
			
			    if(strcmp("RAIL_POS_0",commande)==0)
			    {
				    printf("CP 0\n");
				    u_act.act.CP = 0;
			    }
			    if(strcmp("RAIL_POS_1",commande)==0)
			    {
				    printf("CP 1\n");
				    u_act.act.CP = 1;
			    }
			    if(strcmp("RAIL_POS_2",commande)==0)
			    {
				    printf("CP 2\n");
				    u_act.act.CP = 2;
			    }
			    if(strcmp("RAIL_POS_3",commande)==0)
			    {
				    printf("CP 3\n");
				    u_act.act.CP = 3;
			    }
			    if(strcmp("LOGOUT",commande) == 0)
			    {
			        printf("Logout !\n");
			        
			        close(conn_desc);
				    goto EndConnection;
			    }
			    write(fd_petra_out, &u_act.byte, 1);
			   
		    }
		    printf("Nouvelle commande !\n");
		
	    }
	    EndConnection:;
	    printf("Déconnecté\n");
	    pthread_mutex_lock(&MutexReading);
	    ReadEnable = 0;
	    pthread_mutex_unlock(&MutexReading);
	}
    while(1);
    
	return NULL;
}
