using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP;

namespace TGC.MonoGame.niveles{
    public class Nivel3{

        public const string ContentFolder3D = "Models/";
        public Model PisoModel { get; set; }
        public Matrix[] PisoWorlds { get; set; }
        public Model ParedModel { get; set; }
        public Matrix[] ParedWorlds { get; set; }
        public Ball Bola { get; set; }
        public Checkpoint Checkpoint { get; set; }
        public Ovni Ovnis { get; set; }
        public Pulpito Pulpito { get; set; }
        public Cartel Carteles { get; set; }
        public PowerUps PowerUps{ get; set; }

        public const float DistanceBetweenFloor = 12.33f;
        public const float DistanceBetweenWall = 18f;
        public Matrix escala = Matrix.CreateScale(0.03f);
        public Vector3 arriba = new Vector3(0f, 3.6f, 0f);
        public Vector3 alturaPisoPared = new(0f, 3.6f, 0f);
        public Vector3 alturaEscalera = new Vector3(0f, 6f, 0f);
        public const float distanciaEscaleras = 3f;
        
        // ____ World matrices ____
        //matrices de las plataformas fijas (pisos)
        //matrices tipo lista para que tengan los pisos flotantes
         

        public Nivel3() {

            Ovnis = new Ovni();
            Bola = new Ball(new (0f,200f,0f));
            Pulpito = new Pulpito();
            Checkpoint = new Checkpoint();
            Carteles = new Cartel();
            PowerUps = new PowerUps();

            Initialize();
        }
        
        private void Initialize() {
            //PisoWorld = Matrix.Identity * Matrix.CreateScale(.05f);
            PisoWorlds = new Matrix[]{
                // Matrix.Identity * Matrix.CreateScale(0.1f),
                // Matrix.CreateTranslation(411f, 50f, 411f)* Matrix.CreateScale(0.1f),
                
                //base de incio (hay que hacer una funcion que se encargue de recibir las posiciones y te devuelve la base esta)
                escala * Matrix.Identity,
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Backward + Vector3.Right) * DistanceBetweenFloor),
                escala * Matrix.CreateTranslation((Vector3.Backward + Vector3.Left) * DistanceBetweenFloor),
                
                //Camino inicial
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor) * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor * 2),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor * 2),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor) * 3),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor * 3),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor * 3),
                escala * Matrix.CreateTranslation(Vector3.Forward * (DistanceBetweenFloor) * 4),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenFloor * 4),
                escala * Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor * 4),

                
            };

            ParedWorlds = new Matrix[]{
                // Pared inicio
                
                //Pared Izquierda
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + Vector3.Backward * DistanceBetweenFloor + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + Vector3.Forward * DistanceBetweenFloor + alturaPisoPared),
                
                // Pared derecha
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + Vector3.Backward * DistanceBetweenFloor + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Right * DistanceBetweenWall + Vector3.Forward * DistanceBetweenFloor + alturaPisoPared),
                
                // Pared atras
                escala * Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + alturaPisoPared),
                escala * Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + Vector3.Right * DistanceBetweenFloor + alturaPisoPared),
                escala * Matrix.CreateRotationY(1.5708f) *
                    Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenWall + Vector3.Left * DistanceBetweenFloor + alturaPisoPared),
               
                
                // Pared Caminos
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + Vector3.Backward * DistanceBetweenFloor * 2 + alturaPisoPared),
                escala * Matrix.CreateTranslation(Vector3.Left * DistanceBetweenWall + Vector3.Forward * DistanceBetweenFloor * 2 + alturaPisoPared),
                
                
                
            };
            Ovnis.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 2 + alturaEscalera * 3);
            Ovnis.agregarOvni(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 7 + alturaEscalera * 3);
            Pulpito.agregarPulpito((Vector3.Forward + Vector3.Left) * DistanceBetweenFloor);
            Checkpoint.AgregoCheckpoint(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 10 + alturaEscalera * 3);
            Carteles.AgregarCartel(Vector3.Forward * DistanceBetweenFloor * 3 + Vector3.Left * DistanceBetweenFloor / 2 + arriba * 2);
            Carteles.AgregarCartel(Vector3.Forward * (DistanceBetweenFloor * 6.25f + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 4 + alturaEscalera * 3 + arriba * 2);
            PowerUps.agregarPowerUp(Vector3.Forward * (DistanceBetweenFloor * 7 + distanciaEscaleras * 3) + Vector3.Left * DistanceBetweenFloor * 3 + alturaEscalera * 3);
        }

        public void LoadContent(ContentManager Content){
            PisoModel = Content.Load<Model>(ContentFolder3D + "shared/Ceiling");
            ParedModel = Content.Load<Model>(ContentFolder3D + "shared/Wall");
            Bola.LoadContent(Content);
            Checkpoint.LoadContent(Content);
            Ovnis.LoadContent(Content);
            Pulpito.LoadContent(Content);
            Carteles.LoadContent(Content);
            PowerUps.LoadContent(Content);
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection){

            //PisoModel.Draw(PisoWorlds, view, projection);
            for(int i=0; i < PisoWorlds.Length; i++){
                Matrix _pisoWorld = PisoWorlds[i];
                PisoModel.Draw(_pisoWorld, view, projection);
            }

            for(int i=0; i < ParedWorlds.Length; i++){
                Matrix _paredWorld = ParedWorlds[i];
                ParedModel.Draw(_paredWorld, view, projection);
                
            }

            Bola.Draw(gameTime, view, projection);
            Checkpoint.Draw(gameTime, view, projection);
            Ovnis.Draw(gameTime, view, projection);
            Pulpito.Draw(gameTime, view, projection);
            Carteles.Draw(gameTime, view, projection);
            PowerUps.Draw(gameTime, view, projection);

        }
    
    }
}