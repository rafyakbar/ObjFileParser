using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ObjFileParser
{
    public partial class GUI : Form
    {
        List<string> rows;

        List<string> vertices;
        List<string> tex_coords;
        List<string> normals;
        List<string> faces;

        //bug inisialisasi array faces pada c++
        //faces[][inisisalisasi ini yg ngebug, belum dihitung][3]

        string file_name;

        public GUI()
        {
            InitializeComponent();
        }

        // mengambil data obj dari user dan mengembalikannya
        private List<string> browse()
        {
            try
            {
                OpenFileDialog chooseFile = new OpenFileDialog();

                chooseFile.InitialDirectory = "D:\\";
                chooseFile.Filter = "obj files (*.obj)|*.obj";
                chooseFile.FilterIndex = 1;
                chooseFile.RestoreDirectory = true;
                chooseFile.Multiselect = false;

                if (chooseFile.ShowDialog() == DialogResult.OK)
                {
                    infoTextBox.Text = chooseFile.FileName;
                    file_name = chooseFile.SafeFileName.ToLower().Replace(' ', '_').Replace(".obj", "");
                    try
                    {
                        return File.ReadAllLines(chooseFile.FileName).ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ngebug :v -> " + ex.ToString());
            }

            return null;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            try
            {
                rows = browse();
                // inisialisasi awal
                vertices = new List<string>();
                tex_coords = new List<string>();
                normals = new List<string>();
                faces = rows.Where(i => i.Split(' ')[0] == "f").ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ngebug :v -> " + ex.ToString());
            }
        }

        private void parseButton_Click(object sender, EventArgs e)
        {
            try
            {
                // menghitung progressbar max progressbar
                progressBar.Maximum = faces.Count + rows.Where(i => i.Split(' ')[0] == "v").ToList().Count + 1;
                progressBar.Step = 1;
                progressBar.Value = 0;
                if (glTexCoord3f.Checked)
                {
                    progressBar.Maximum += rows.Where(i => i.Split(' ')[0] == "vt").ToList().Count;
                }
                if (glNormal3f.Checked)
                {
                    progressBar.Maximum += rows.Where(i => i.Split(' ')[0] == "vn").ToList().Count;
                }

                string result = "void " + file_name + "() {" + Environment.NewLine;
                List<string> dumpList;

                dumpList = rows.Where(i => i.Split(' ')[0] == "v").ToList();
                result += "\tfloat vertices[][3] = {" + Environment.NewLine;
                foreach (string vertex in dumpList)
                {
                    string dump = vertex.Replace("   ", " ").Replace("  ", " ").Replace("v ", "");
                    dump = (dump[dump.Length - 1] == ' ') ? dump.Remove(dump.Length - 1) : dump;
                    string[] column = dump.Split(' ');

                    result += "\t\t{" + column[0] + ", " + column[1] + ", " + column[2] + "}," + Environment.NewLine;

                    progressBar.Value++;
                }
                result += "\t};" + Environment.NewLine;

                // menambahkan texcoord dicentang 
                if (glTexCoord3f.Checked)
                {
                    dumpList = rows.Where(i => i.Split(' ')[0] == "vt").ToList();
                    result += "\tfloat tex_coords[][3] = {" + Environment.NewLine;
                    foreach (string coord in dumpList)
                    {
                        string dump = coord.Replace("   ", " ").Replace("  ", " ").Replace("vt ", "");
                        dump = (dump[dump.Length - 1] == ' ') ? dump.Remove(dump.Length - 1) : dump;
                        string[] column = dump.Split(' ');

                        if (column.Length == 3)
                        {
                            result += "\t\t{" + column[0] + ", " + column[1] + ", " + column[2] + "}," + Environment.NewLine;
                        }
                        else
                        {
                            result += "\t\t{" + column[0] + ", " + column[1] + "}," + Environment.NewLine;
                        }

                        progressBar.Value++;
                    }
                    result += "\t};" + Environment.NewLine;
                }

                //menambahkan normal jika dicentang
                if (glNormal3f.Checked)
                {
                    dumpList = rows.Where(i => i.Split(' ')[0] == "vn").ToList();
                    result += "\tfloat normals[][3] = {" + Environment.NewLine;
                    foreach (string normal in dumpList)
                    {
                        string dump = normal.Replace("   ", " ").Replace("  ", " ").Replace("vn ", "");
                        dump = (dump[dump.Length - 1] == ' ') ? dump.Remove(dump.Length - 1) : dump;
                        string[] column = dump.Split(' ');

                        if (column.Length == 3)
                        {
                            result += "\t\t{" + column[0] + ", " + column[1] + ", " + column[2] + "}," + Environment.NewLine;
                        }
                        else
                        {
                            result += "\t\t{" + column[0] + ", " + column[1] + "}," + Environment.NewLine;
                        }

                        progressBar.Value++;
                    }
                    result += "\t};" + Environment.NewLine;
                }

                dumpList = rows.Where(i => i.Split(' ')[0] == "f").ToList();
                int max = dumpList.OrderBy(i => i.Split(' ').Length).ToList()[0].Split(' ').Length;
                result += "\tint faces[][" + max + "][3] = {" + Environment.NewLine;
                foreach (string face in dumpList)
                {
                    string dump = face.Replace("   ", " ").Replace("  ", " ").Replace("f ", "");
                    dump = (dump[dump.Length - 1] == ' ') ? dump.Remove(dump.Length - 1) : dump;
                    string[] column = dump.Split(' ');

                    result += "\t\t{"; 
                    foreach(string f in column)
                    {
                        string[] field = f.Split('/');
                        result += "{" + field[0] + ", " + ((field[1] == "") ? "NULL" : field[1]) + ", " + ((field.Length < 3) ? "NULL" : field[2]) + "}, ";
                    }

                    progressBar.Value++;
                    result += "}," + Environment.NewLine;
                }
                result += "\t};" + Environment.NewLine;

                //for 1
                result += "\tfor (int i = 0; i < (sizeof(faces))/ sizeof(faces[0]); ++i) {" + Environment.NewLine;

                result += "\t\tglBegin(GL_POLYGON);" + Environment.NewLine;

                //for 2
                result += "\t\tfor (int j = 0; j < (sizeof(faces[0]))/ sizeof(faces[0][0]); ++j) {" + Environment.NewLine;

                result += "\t\t\tif (faces[i][j][0] != NULL) {" + Environment.NewLine;

                if (glTexCoord3f.Checked)
                {
                    result += "\t\t\t\tif (faces[i][j][1] != NULL) {" + Environment.NewLine;
                    result += "\t\t\t\t\tif(tex_coords[faces[i][j][1] - 1][2] == NULL)" + Environment.NewLine;
                    result += "\t\t\t\t\t\tglTexCoord2f(tex_coords[faces[i][j][1] - 1][0], tex_coords[faces[i][j][1] - 1][1]);" + Environment.NewLine;
                    result += "\t\t\t\t\telse" + Environment.NewLine;
                    result += "\t\t\t\t\t\tglTexCoord3fv(tex_coords[faces[i][j][1] - 1]);" + Environment.NewLine;
                    result += "\t\t\t\t}" + Environment.NewLine;
                }

                if (glNormal3f.Checked)
                {
                    result += "\t\t\t\tif (faces[i][j][2] != NULL) {" + Environment.NewLine;
                    result += "\t\t\t\t\tglNormal3fv(normals[faces[i][j][2] - 1]);" + Environment.NewLine;
                    result += "\t\t\t\t}" + Environment.NewLine;
                }

                result += "\t\t\t\tglVertex3fv(vertices[faces[i][j][0] - 1]);" + Environment.NewLine;

                result += "\t\t\t}" + Environment.NewLine;

                //endfor 2
                result += "\t\t}" + Environment.NewLine;

                result += "\t\tglEnd();" + Environment.NewLine;

                //endfor 1
                result += "\t}" + Environment.NewLine;

                result += "}";

                File.WriteAllText(file_name + ".txt", result);
                progressBar.Value++;

                MessageBox.Show("Selesai!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("ngebug :v -> " + ex.ToString());
            }
        }
    }
}
