using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result AddLINQ(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.FMirandaExamenBecasEntities context = new DL_EF.FMirandaExamenBecasEntities())
                {
                    
                    DL_EF.Alumno alumnoDL = new DL_EF.Alumno();

                    alumnoDL.Nombre = alumno.NombreAlumno;
                    alumnoDL.ApellidoPaterno = alumno.ApellidoPaterno;
                    alumnoDL.ApellidoMaterno = alumno.ApellidoMaterno;
                   
                    alumnoDL.IdBeca = alumno.Beca.IdBeca;


                    context.Alumnoes.Add(alumnoDL);
                    context.SaveChanges();


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el alumno" + result.Ex;

                throw;
            }
            return result;

        }

        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.FMirandaExamenBecasEntities context = new DL_EF.FMirandaExamenBecasEntities())
                {
                    
                    var query = (from alumnoLINQ in context.Alumnoes

                                 select new
                                 {

                                     IdAlumno = alumnoLINQ.IdAlumno,

                                     NombreAlumno = alumnoLINQ.NombreAlumno,
                                     ApellidoPaterno = alumnoLINQ.ApellidoPaterno,
                                     ApellidoMaterno = alumnoLINQ.ApellidoMaterno,
                                    
                                     IdBeca = alumnoLINQ.IdBeca
                                 });
                    //var query = (from alumnoLINQ in context.Alumnoes
                    //             where alumnoLINQ.IdAlumno == idAlumno
                    //             select new
                    //             {

                    //                 IdAlumno = alumnoLINQ.IdAlumno,

                    //                 Nombre = alumnoLINQ.Nombre,
                    //                 ApellidoPaterno = alumnoLINQ.ApellidoPaterno,
                    //                 ApellidoMaterno = alumnoLINQ.ApellidoMaterno,
                    //               
                    //                 IdSemestre = alumnoLINQ.IdSemestre
                    //             }); 
                    //GETBYID 

                    if (query != null && query.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var row in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();

                            alumno.IdAlumno = row.IdAlumno;
                            alumno.NombreAlumno = row.Nombre;
                            alumno.ApellidoPaterno = row.ApellidoPaterno;
                            alumno.ApellidoMaterno = row.ApellidoMaterno;
                            

                            alumno.Beca = new ML.Becas();
                            alumno.Beca.IdBeca = row.IdBeca  .Value;

                            result.Objects.Add(alumno); //boxing y unboxing

                        }
                    }

                }
                result.Correct = true;
            }//codigo que puede causar una excepcion 
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el alumno" + result.Ex;

                throw;
            }//manejo de excepciones 

            return result;
        }


    }
}
