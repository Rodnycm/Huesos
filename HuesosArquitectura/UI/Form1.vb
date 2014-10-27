Public Class Form1

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim nombre As String = txtNombre.Text
        Dim tipo As String = txtTipo.Text
        Dim ubicacion As String = txtUbicacion.Text

        gestorHueso.agregarHueso(nombre, tipo, ubicacion)

        gestorHueso.guardarCambios()

        
    End Sub
End Class
