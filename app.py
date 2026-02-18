from flask import Flask, render_template, request, redirect, url_for
from flask_mysqldb import MySQL

app = Flask(__name__)

app.config['MYSQL_HOST'] = 'localhost'
app.config['MYSQL_USER'] = 'root'
app.config['MYSQL_PASSWORD'] = ''
app.config['MYSQL_DB'] = 'crud' 

mysql = MySQL(app) 

@app.route('/')
def index():    
    cur = mysql.connection.cursor()
    cur.execute('SELECT * FROM usuarios')
    data = cur.fetchall()
    cur.close()
    return render_template('index.html', usuarios = data)


@app.route('/agregar', methods=['POST'])
def agregar_usuario():
    if request.method == 'POST':
        gmail = request.form['gmail']
        contrasena = request.form['contrasena']
        nombre = request.form['nombre']
        apellido = request.form['apellido']
         
        cur = mysql.connection.cursor()
        cur.execute('INSERT usuarios(gmail, contrasena, nombre, apellido) VALUES(%s, %s, %s, %s)', (gmail, contrasena, nombre, apellido))
        mysql.connection.commit()
        cur.close()
        return redirect(url_for('index'))


@app.route('/eliminar/<int:id>')
def eliminar_usuario(id):
    cur = mysql.connection.cursor()
    cur.execute('DELETE FROM usuarios WHERE id = %s', (id,))
    mysql.connection.commit()
    cur.close()
    return redirect(url_for('index'))


@app.route('/editar/<int:id>', methods=['POST', 'GET'])
def editar_usuario(id):
    if request.method == 'POST':
        gmail = request.form['gmail']
        contrasena = request.form['contrasena']
        nombre = request.form['nombre']
        apellido = request.form['apellido']

        cur = mysql.connection.cursor()
        cur.execute('UPDATE usuarios SET gmail = %s, contrasena = %s, nombre = %s, apellido = %s WHERE id = %s', (gmail, contrasena, nombre, apellido, id,))
        mysql.connection.commit()   
        cur.close()
        return redirect(url_for('index'))
    else:
        cur = mysql.connection.cursor()
        cur.execute('SELECT * FROM usuarios WHERE id = %s', (id,))
        data = cur.fetchone()
        cur.close()
        return render_template('editar.html', usuario = data)
    


if __name__ == '__main__':
    app.run(debug=True)