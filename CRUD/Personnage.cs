using System;
using System.Data.SqlClient;
using System.Data;

namespace CRUD
{
    class Personnage
    {
        private int id;
        private string Vigueur;
        private string Agilite;        
        #region Encapsulation
        public int Id { get => id; set => id = value; }
        public string Nom { get => Nom; set => Nom = value; }

        #endregion Encapsulation

        // Constructur
        public Personnage() { }
        public Personnage(int id, string nom, string l_name, string city, string dep)
        {
            this.Id = id;
            this.Nom = nom;

        }

        // Add Personnage Method
        public int AddPersonnage(ADO db)
        {
            return this.ExecuteProcedure(db, "ADD_P");
        }

        // Update Personnage Method
        public int UpdatePersonnage(ADO db)
        {
            return this.ExecuteProcedure(db, "UPDATE_P");
        }
        
        // Delete Personnage Method
        public int DeletePersonnage(ADO db)
        {
            return this.ExecuteProcedure(db, "DELETE_P");
        }

        // Execute Procdure Method
        private int ExecuteProcedure(ADO db, string procedureName)
        {
            try
            {
                db.Cmd.CommandType = CommandType.StoredProcedure;
                db.Cmd.CommandText = procedureName;

                if (procedureName == "ADD_P" || procedureName == "UPDATE_P")
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@Id", this.Id);
                    parameters[1] = new SqlParameter("@Nom", this.Nom);

                    db.Cmd.Parameters.Clear();
                    foreach (SqlParameter p in parameters)
                    {
                        p.Direction = ParameterDirection.Input;
                        db.Cmd.Parameters.Add(p);
                    }
                }
                else if (procedureName == "DELETE_P")
                {
                    SqlParameter idPar = new SqlParameter("@Id", this.Id);
                    db.Cmd.Parameters.Clear();
                    idPar.Direction = ParameterDirection.Input;
                    db.Cmd.Parameters.Add(idPar);
                }

                SqlParameter ok = new SqlParameter("@done", SqlDbType.Int);
                ok.Direction = ParameterDirection.Output;
                db.Cmd.Parameters.Add(ok);

                db.Cmd.ExecuteNonQuery();
                return int.Parse( ok.Value.ToString() );
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return -1;
            }
        }

    }
}
