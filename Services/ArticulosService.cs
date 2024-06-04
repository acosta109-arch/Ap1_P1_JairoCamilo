using Ap1_P1_JairoCamilo.DAL;
using Ap1_P1_JairoCamilo.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ap1_P1_JairoCamilo.Services;

public class ArticulosService
{
    private readonly Contexto _contexto;

    public ArticulosService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Articulos.AnyAsync(a => a.ArticuloId == id);
    }

    private async Task<bool> Insertar(Articulos articulo)
    {
        _contexto.Articulos.Add(articulo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Articulos articulo)
    {
        _contexto.Articulos.Update(articulo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Articulos articulo)
    {
        if (!await Existe(articulo.ArticuloId))
        {
            return await Insertar(articulo);
        }
        else
        {
            return await Modificar(articulo);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        var eliminar = await _contexto.Articulos
            .Where(t => t.ArticuloId == id)
            .ExecuteDeleteAsync();
        return eliminar > 0;
    }

    public async Task<Articulos?> Buscar(int id)
    {
        return await _contexto.Articulos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.ArticuloId == id);
    }

    public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
    {
        return await _contexto.Articulos.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteArticuloConDescripcion(string descripcion)
    {
        return await _contexto.Articulos.AnyAsync(a => a.Descripcion == descripcion);
    }

    public decimal CalcularCostoConGanancia(decimal costo, decimal porcentajeGanancia)
    {
        return costo + (costo * porcentajeGanancia / 100);
    }
}
