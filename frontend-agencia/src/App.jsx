import { useState, useEffect } from 'react'
import { BarChart, Bar, XAxis, YAxis, Tooltip, ResponsiveContainer, PieChart, Pie, Cell, CartesianGrid, Legend } from 'recharts';
import Swal from 'sweetalert2';
import './App.css'

function App() {
    const [clientes, setClientes] = useState([])
    const [proyectos, setProyectos] = useState([])
    const [stats, setStats] = useState(null)

    const [nuevoCliente, setNuevoCliente] = useState({ id: 0, nombre: '', apellido: '', email: '', telefono: '' })
    const [nuevoProyecto, setNuevoProyecto] = useState({ id: 0, nombre: '', descripcion: '', precio: 0, clienteId: 1 })

    const [modoEdicionCli, setModoEdicionCli] = useState(false)
    const [modoEdicionPro, setModoEdicionPro] = useState(false)
    const [vista, setVista] = useState('clientes')
    const [filtroCliente, setFiltroCliente] = useState(null)

    const API_URL = "http://localhost:5244/api";

    const COLORS = ['#4f46e5', '#10b981', '#f59e0b', '#ef4444', '#8b5cf6'];

    const cargarDatos = async () => {
        try {
            const resCli = await fetch(`${API_URL}/clientes`);
            if (resCli.ok) setClientes(await resCli.json());

            const resPro = await fetch(`${API_URL}/proyectos`);
            if (resPro.ok) setProyectos(await resPro.json());

            const resStats = await fetch(`${API_URL}/dashboard`);
            if (resStats.ok) setStats(await resStats.json());
        } catch (e) { console.error("Error cargando API"); }
    }

    useEffect(() => { cargarDatos() }, [])

    // --- LOGICA CRUD ---
    const guardarCliente = async (e) => {
        e.preventDefault();
        const endpoint = modoEdicionCli ? `${API_URL}/clientes/${nuevoCliente.id}` : `${API_URL}/clientes`;
        const metodo = modoEdicionCli ? 'PUT' : 'POST';
        await fetch(endpoint, { method: metodo, headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(nuevoCliente) });

        Swal.fire({ position: 'top-end', icon: 'success', title: modoEdicionCli ? 'Cliente actualizado' : 'Cliente guardado', showConfirmButton: false, timer: 1500 });
        setNuevoCliente({ id: 0, nombre: '', apellido: '', email: '', telefono: '' });
        setModoEdicionCli(false); cargarDatos();
    }

    const eliminarCliente = (id) => {
        Swal.fire({
            title: '¿Eliminar Cliente?', text: "Se borrarán también sus proyectos.", icon: 'warning', showCancelButton: true, confirmButtonColor: '#ef4444', cancelButtonColor: '#6b7280', confirmButtonText: 'Sí, eliminar'
        }).then(async (result) => {
            if (result.isConfirmed) {
                await fetch(`${API_URL}/clientes/${id}`, { method: 'DELETE' });
                cargarDatos();
                Swal.fire('¡Eliminado!', '', 'success');
            }
        })
    }

    const guardarProyecto = async (e) => {
        e.preventDefault();
        const endpoint = modoEdicionPro ? `${API_URL}/proyectos/${nuevoProyecto.id}` : `${API_URL}/proyectos`;
        const metodo = modoEdicionPro ? 'PUT' : 'POST';
        await fetch(endpoint, { method: metodo, headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(nuevoProyecto) });

        Swal.fire({ position: 'top-end', icon: 'success', title: 'Proyecto guardado', showConfirmButton: false, timer: 1500 });
        setNuevoProyecto({ id: 0, nombre: '', descripcion: '', precio: 0, clienteId: 1 });
        setModoEdicionPro(false); cargarDatos();
    }

    const eliminarProyecto = (id) => {
        Swal.fire({ title: '¿Borrar Proyecto?', icon: 'warning', showCancelButton: true, confirmButtonColor: '#ef4444', confirmButtonText: 'Sí, borrar' }).then(async (result) => {
            if (result.isConfirmed) {
                await fetch(`${API_URL}/proyectos/${id}`, { method: 'DELETE' });
                cargarDatos();
                Swal.fire('Borrado', '', 'success');
            }
        })
    }

    const verProyectosDe = (id) => {
        setFiltroCliente(id);
        setVista('proyectos');
        setNuevoProyecto(prev => ({ ...prev, clienteId: id }));
    }

    const proyectosVisibles = filtroCliente ? proyectos.filter(p => p.clienteId === filtroCliente) : proyectos;

    const datosGrafico = proyectos.map(p => ({
        name: p.nombre.length > 10 ? p.nombre.substring(0, 10) + '...' : p.nombre,
        precio: p.precio
    }));

    return (
        <div className="container" style={{ maxWidth: '1600px' }}> {/* Aumenté el ancho máximo para que quepa todo */}
            <h1 style={{ marginBottom: '20px' }}>⚡ Agencia Manager</h1>

            {/* === LAYOUT MAESTRO: 2 COLUMNAS (IZQ: DATOS | DER: GRAFICOS) === */}
            <div style={{ display: 'grid', gridTemplateColumns: '2fr 1fr', gap: '30px', alignItems: 'start' }}>

                {/* ============ COLUMNA IZQUIERDA (OPERATIVA) ============ */}
                <div className="left-panel">

                    {/* 1. TARJETAS DE ESTADISTICAS (En fila) */}
                    {stats && (
                        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: '15px', marginBottom: '30px' }}>
                            <div className="stat-card">
                                <div className="stat-label">Ingresos</div>
                                <div className="stat-value" style={{ color: '#10b981', fontSize: '1.4rem' }}>${stats.ingresosTotales.toLocaleString()}</div>
                            </div>
                            <div className="stat-card">
                                <div className="stat-label">Clientes</div>
                                <div className="stat-value" style={{ fontSize: '1.4rem' }}>{stats.totalClientes}</div>
                            </div>
                            <div className="stat-card">
                                <div className="stat-label">Proyectos</div>
                                <div className="stat-value" style={{ fontSize: '1.4rem' }}>{stats.totalProyectos}</div>
                            </div>
                            <div className="stat-card">
                                <div className="stat-label">Top Cliente</div>
                                <div className="stat-value" style={{ fontSize: '1.1rem' }}>{stats.clienteMasFrecuente || "N/A"}</div>
                            </div>
                        </div>
                    )}

                    {/* 2. BOTONES DE NAVEGACIÓN */}
                    <div style={{ marginBottom: '20px', display: 'flex', gap: '10px' }}>
                        <button className={vista === 'clientes' ? 'btn-primary' : 'btn-secondary'} onClick={() => setVista('clientes')} style={{ width: 'auto' }}>👥 Gestión de Clientes</button>
                        <button className={vista === 'proyectos' ? 'btn-primary' : 'btn-secondary'} onClick={() => setVista('proyectos')} style={{ width: 'auto' }}>📂 Gestión de Proyectos</button>
                    </div>

                    {/* 3. ZONA CRUD (FORMULARIO Y LISTA) */}
                    <div className="crud-area" style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '20px' }}>

                        {/* A. FORMULARIOS */}
                        <div className="form-column">
                            {vista === 'clientes' ? (
                                <div className="form-card" style={{ borderTop: '4px solid #4f46e5' }}>
                                    <h3>{modoEdicionCli ? "✏️ Editar Cliente" : "✨ Nuevo Cliente"}</h3>
                                    <form onSubmit={guardarCliente}>
                                        <input placeholder="Nombre" value={nuevoCliente.nombre} onChange={e => setNuevoCliente({ ...nuevoCliente, nombre: e.target.value })} required />
                                        <input placeholder="Apellido" value={nuevoCliente.apellido} onChange={e => setNuevoCliente({ ...nuevoCliente, apellido: e.target.value })} required />
                                        <input placeholder="Email" value={nuevoCliente.email} onChange={e => setNuevoCliente({ ...nuevoCliente, email: e.target.value })} required />
                                        <input placeholder="Teléfono" value={nuevoCliente.telefono} onChange={e => setNuevoCliente({ ...nuevoCliente, telefono: e.target.value })} />
                                        <button type="submit" className="btn-primary">{modoEdicionCli ? "Actualizar" : "Guardar Cliente"}</button>
                                        {modoEdicionCli && <button type="button" onClick={() => { setModoEdicionCli(false); setNuevoCliente({ id: 0, nombre: '', apellido: '', email: '', telefono: '' }) }} style={{ marginTop: '10px', background: 'transparent', color: '#666' }}>Cancelar</button>}
                                    </form>
                                </div>
                            ) : (
                                <div className="form-card" style={{ borderTop: '4px solid #10b981' }}>
                                    <h3>{modoEdicionPro ? "✏️ Editar Proyecto" : "🚀 Nuevo Proyecto"}</h3>
                                    <form onSubmit={guardarProyecto}>
                                        <input placeholder="Nombre Proyecto" value={nuevoProyecto.nombre} onChange={e => setNuevoProyecto({ ...nuevoProyecto, nombre: e.target.value })} required />
                                        <input placeholder="Descripción" value={nuevoProyecto.descripcion} onChange={e => setNuevoProyecto({ ...nuevoProyecto, descripcion: e.target.value })} />
                                        <input type="number" placeholder="Precio ($)" value={nuevoProyecto.precio} onChange={e => setNuevoProyecto({ ...nuevoProyecto, precio: Number(e.target.value) })} required />
                                        <select value={nuevoProyecto.clienteId} onChange={e => setNuevoProyecto({ ...nuevoProyecto, clienteId: Number(e.target.value) })}>
                                            {clientes.map(c => <option key={c.id} value={c.id}>{c.nombre} {c.apellido}</option>)}
                                        </select>
                                        <button type="submit" className="btn-primary" style={{ marginTop: '15px', background: '#10b981' }}>{modoEdicionPro ? "Actualizar" : "Crear Proyecto"}</button>
                                        {modoEdicionPro && <button type="button" onClick={() => { setModoEdicionPro(false); setNuevoProyecto({ id: 0, nombre: '', descripcion: '', precio: 0, clienteId: 1 }) }} style={{ marginTop: '10px', background: 'transparent', color: '#666' }}>Cancelar</button>}
                                    </form>
                                </div>
                            )}
                        </div>

                        {/* B. LISTAS */}
                        <div className="list-column">
                            {vista === 'clientes' && (
                                <div className="items-container" style={{ display: 'grid', gap: '10px' }}>
                                    {clientes.map(c => (
                                        <div key={c.id} className="item-card" style={{ borderLeft: '4px solid #4f46e5' }}>
                                            <div className="card-header">
                                                <h4 className="card-title">{c.nombre} {c.apellido}</h4>
                                                <div className="actions">
                                                    <a href={`mailto:${c.email}`} className="icon-btn" style={{ background: '#e0e7ff', color: '#4f46e5', textDecoration: 'none', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>✉️</a>
                                                    <button className="icon-btn btn-edit" onClick={() => { setNuevoCliente(c); setModoEdicionCli(true) }}>✏️</button>
                                                    <button className="icon-btn btn-delete" onClick={() => eliminarCliente(c.id)}>🗑️</button>
                                                </div>
                                            </div>
                                            <p className="card-subtitle">{c.email}</p>
                                            <button className="btn-projects" onClick={() => verProyectosDe(c.id)}>📂 Ver sus Proyectos</button>
                                        </div>
                                    ))}
                                </div>
                            )}

                            {vista === 'proyectos' && (
                                <div>
                                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '10px' }}>
                                        <h3 style={{ margin: 0, fontSize: '1rem' }}>{filtroCliente ? `Cliente #${filtroCliente}` : "Todos"}</h3>
                                        {filtroCliente && <button className="btn-secondary" onClick={() => setFiltroCliente(null)} style={{ width: 'auto', padding: '5px 10px', fontSize: '0.8rem' }}>Ver Todos</button>}
                                    </div>
                                    <div className="items-container" style={{ display: 'grid', gap: '10px' }}>
                                        {proyectosVisibles.map(p => (
                                            <div key={p.id} className="item-card" style={{ borderLeft: '4px solid #10b981' }}>
                                                <div className="card-header">
                                                    <h4 className="card-title">{p.nombre}</h4>
                                                    <div className="actions">
                                                        <button className="icon-btn btn-edit" onClick={() => { setNuevoProyecto(p); setModoEdicionPro(true) }}>✏️</button>
                                                        <button className="icon-btn btn-delete" onClick={() => eliminarProyecto(p.id)}>🗑️</button>
                                                    </div>
                                                </div>
                                                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                                                    <span style={{ fontWeight: 'bold', color: '#10b981' }}>${p.precio}</span>
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            )}
                        </div>

                    </div>
                </div>

                {/* ============ COLUMNA DERECHA (GRAFICOS FIJOS) ============ */}
                <div className="right-panel" style={{ display: 'flex', flexDirection: 'column', gap: '20px', position: 'sticky', top: '20px' }}>

                    {/* Gráfico 1: Dona */}
                    <div className="stat-card" style={{ height: '350px', padding: '10px', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                        <h3 style={{ marginTop: '10px', color: '#6b7280', fontSize: '1rem' }}>Distribución de Presupuesto</h3>
                        <ResponsiveContainer width="100%" height="100%">
                            <PieChart>
                                <Pie data={datosGrafico} cx="50%" cy="50%" innerRadius={70} outerRadius={90} paddingAngle={5} dataKey="precio">
                                    {datosGrafico.map((entry, index) => (
                                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                                    ))}
                                </Pie>
                                <Tooltip />
                                <Legend verticalAlign="bottom" height={36} />
                            </PieChart>
                        </ResponsiveContainer>
                    </div>

                    {/* Gráfico 2: Barras */}
                    <div className="stat-card" style={{ height: '350px', padding: '10px' }}>
                        <h3 style={{ marginTop: '10px', marginBottom: '20px', color: '#6b7280', fontSize: '1rem', textAlign: 'center' }}>Comparativa de Ingresos</h3>
                        <ResponsiveContainer width="100%" height="90%">
                            <BarChart data={datosGrafico}>
                                <CartesianGrid strokeDasharray="3 3" vertical={false} />
                                <XAxis dataKey="name" tick={{ fontSize: 10 }} interval={0} angle={-15} textAnchor="end" />
                                <YAxis tick={{ fontSize: 12 }} />
                                <Tooltip cursor={{ fill: '#f3f4f6' }} contentStyle={{ borderRadius: '8px' }} />
                                <Bar dataKey="precio" fill="#4f46e5" radius={[4, 4, 0, 0]} />
                            </BarChart>
                        </ResponsiveContainer>
                    </div>

                </div>

            </div>
        </div>
    )
}

export default App