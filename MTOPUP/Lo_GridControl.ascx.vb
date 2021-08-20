
Imports System.Data.SqlClient
Imports System.Data
Partial Class Lo_GridControl
    Inherits System.Web.UI.UserControl
    Property BindingErrorReport As String
    Event GridBindCompleted(sender As Object, e As GridViewRowEventArgs)
    Event GridSorting(sender As Object, e As GridViewSortEventArgs)
    Event GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs)
    Event GridRowClick(sender As Object, e As EventArgs)
    Event GridViewRowCreated(sender As Object, e As GridViewRowEventArgs)
    Property EnablePaging As Boolean
        Get
            Return GridView1.AllowPaging
        End Get
        Set(value As Boolean)
            GridView1.AllowPaging = value
        End Set
    End Property
    Property EnableSorting As Boolean
        Get
            Return GridView1.AllowSorting
        End Get
        Set(value As Boolean)
            GridView1.AllowSorting = value
        End Set
    End Property
    Property ItemsPerPage As Integer
        Get
            Return GridView1.PageSize
        End Get
        Set(value As Integer)
            GridView1.PageSize = value
        End Set
    End Property
    Property RequestSource As DataTable
        Get
            Return GridView1.DataSource
        End Get
        Set(value As DataTable)
            Try
                GridView1.DataSource = value
                GridView1.DataBind()
            Catch ex As Exception
                BindingErrorReport = ex.Message
            End Try

        End Set

    End Property
    Property GridControlElement As GridView
        Get
            Return GridView1
        End Get
        Set(value As GridView)
            GridView1 = value
        End Set
    End Property
    Property GirdPageSize As Integer
        Get
            Return GridView1.PageSize
        End Get
        Set(value As Integer)
            GridView1.PageSize = value
        End Set
    End Property
    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        RaiseEvent GridViewPageIndexChanged(sender, e)
    End Sub

    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            If GridView1.PageIndex < GridView1.PageCount - 1 Then
                Dim PagerTable As Table
                PagerTable = DirectCast(e.Row.Cells(0).Controls(0), Table)
                Dim i As Integer
                For i = 0 To PagerTable.Rows(0).Cells.Count - 1
                    If TypeOf (PagerTable.Rows(0).Cells(i).Controls(0)) Is Label Then
                        Dim PagerLabel As Label
                        PagerLabel = DirectCast(PagerTable.Rows(0).Cells(i).Controls(0), Label)
                        PagerLabel.CssClass = "label label-primary"
                        Exit For
                    End If
                Next
            End If
        End If
        RaiseEvent GridViewRowCreated(sender, e)
    End Sub
    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        RaiseEvent GridBindCompleted(sender, e)
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

        RaiseEvent GridRowClick(sender, e)
    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        RaiseEvent GridSorting(sender, e)
    End Sub
    Public Function GetSortDirection(ByVal column As String) As String

        ' By default, set the sort direction to ascending.
        Dim sortDirection = "ASC"

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirection"), String)
                If lastDirection IsNot Nothing _
                  AndAlso lastDirection = "ASC" Then

                    sortDirection = "DESC"

                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column

        Return sortDirection

    End Function
    Property ColumnsVisible(index As Integer) As Boolean
        Get
            Return GridView1.Columns(index).Visible
        End Get
        Set(value As Boolean)
            GridView1.Columns(index).Visible = value
        End Set
    End Property

End Class