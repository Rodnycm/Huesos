Public Class ListarHuesos

    Private Sub ListarHuesos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstHuesos.DataSource = gestorHueso.consultarHuesos
        lstHuesos.Columns(0).Visible = False
        lstHuesos.Columns(1).HeaderText = "Nombre"
        lstHuesos.Columns(2).HeaderText = "Tipo de hueso"
        lstHuesos.Columns(3).HeaderText = "Ubicación"





    End Sub
End Class