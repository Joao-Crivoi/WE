using DesktopApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class Form1 : Form
    {

        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("https://localhost:7160/api/") };
        private int? selectedUserId = 0;
        public Form1()
        {
            InitializeComponent();
            var btnExcluir = new DataGridViewButtonColumn();
            btnExcluir.HeaderText = "Ação";
            btnExcluir.Text = "Excluir";
            btnExcluir.Name = "btnExcluirColumn";
            btnExcluir.UseColumnTextForButtonValue = true; // Faz o texto aparecer no botão

            // Adiciona a coluna ao DataGridView (se ainda não existir)
            if (!dataGridView1.Columns.Contains("btnExcluirColumn"))
            {
                dataGridView1.Columns.Add(btnExcluir);
            }
            CarregarUsuarios();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario
            {
                Id = selectedUserId,
                Nome = txtNome.Text,
                Senha = txtSenha.Text,
                Status = checkBoxStatus.Checked
            };

            string json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            // SE NÃO TEM ID → É CADASTRO
            if (selectedUserId == 0)
            {
                response = await _client.PostAsync("usuarios", content);
            }
            else  // SE TEM ID → É ATUALIZAÇÃO
            {
                response = await _client.PutAsync($"usuarios/{selectedUserId}", content);
            }

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show(
                    selectedUserId == 0
                        ? "Usuário cadastrado com sucesso!"
                        : "Usuário atualizado com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LimparFormulario();
                CarregarUsuarios();
            }
            else
            {
                var erro = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Erro: " + erro);
            }
        }

        private void LimparFormulario()
        {
            txtNome.Clear();
            txtSenha.Clear();
            checkBoxStatus.Checked = false;
            selectedUserId = 0;
        }
        private async void CarregarUsuarios()
        {
            try
            {
                var response = await _client.GetStringAsync("usuarios");
                var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(response);
                dataGridView1.DataSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar usuários: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                selectedUserId = Convert.ToInt32(row.Cells["Id"].Value);
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtSenha.Text = row.Cells["Senha"].Value.ToString();
                checkBoxStatus.Checked = Convert.ToBoolean(row.Cells["Status"].Value);
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnExcluirColumn" && e.RowIndex >= 0)
            {
                // 1. Confirmação do Usuário
                var resultado = MessageBox.Show("Tem certeza que deseja excluir este usuário?",
                                                 "Confirmação de Exclusão",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {

                    var usuarioParaExcluir = dataGridView1.Rows[e.RowIndex].DataBoundItem as Usuario;
                    int? id = usuarioParaExcluir?.Id;

                    if (id == null || id == 0) return;

                    try
                    {

                        var response = await _client.DeleteAsync($"usuarios/{id}"); // Rota DELETE /api/usuarios/{id}

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Usuário excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LimparFormulario();
                            CarregarUsuarios();
                        }
                        else
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"Erro ao excluir (Status: {response.StatusCode}): {errorContent}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro de conexão com a API durante a exclusão: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LimparFormulario();
            CarregarUsuarios();
        }
    }
}