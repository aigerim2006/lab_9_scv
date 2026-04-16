Imports System.Net.Http
Imports System.Text.Json

Public Class Form1

    ' 1. Функция получения данных (оставляем как есть)
    Public Async Function GetDataFromServerAsync() As Task(Of List(Of DiseaseRecord))
        Using client As New HttpClient()
            Try
                Dim url As String = "http://api.health.kg/v1/diseases"
                Dim response As HttpResponseMessage = Await client.GetAsync(url)

                If response.IsSuccessStatusCode Then
                    Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()
                    Dim options As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
                    Return JsonSerializer.Deserialize(Of List(Of DiseaseRecord))(jsonResponse, options)
                End If
            Catch ex As Exception
                MessageBox.Show("Ошибка сетевого соединения: " & ex.Message)
            End Try
        End Using
        Return New List(Of DiseaseRecord)()
    End Function

    ' 2. ИСПРАВЛЕННЫЙ ОБРАБОТЧИК КЛИКА
    ' Мы привязали его к Button1 (стандартное имя кнопки)
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Вызываем функцию
        Dim diseases = Await GetDataFromServerAsync()

        ' Выводим результат
        If diseases IsNot Nothing AndAlso diseases.Count > 0 Then
            MessageBox.Show($"Успешно загружено записей: {diseases.Count}")
        Else
            MessageBox.Show("Данные не найдены (проверьте URL или интернет).")
        End If
    End Sub

End Class