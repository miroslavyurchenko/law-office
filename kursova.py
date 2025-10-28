import tkinter as tk
from tkinter import ttk

# Функція для додавання адвоката до списку
def add_lawyer():
    initials = initials_entry.get()
    certificate = certificate_entry.get()
    experience = experience_entry.get()
    specialization = specialization_entry.get()

    if initials and certificate and experience and specialization:
        lawyers_tree.insert("", "end", values=(initials, experience, certificate, specialization))
        initials_entry.delete(0, tk.END)
        certificate_entry.delete(0, tk.END)
        experience_entry.delete(0, tk.END)
        specialization_entry.delete(0, tk.END)

# Головне вікно
root = tk.Tk()
root.title("Адвокатська контора")
root.geometry("500x500")

# Заголовок
title = tk.Label(root, text="Адвокатська контора", font=("Arial", 16, "bold"))
title.pack(pady=10)

# Кнопки меню
menu_frame = tk.Frame(root)
menu_frame.pack()

tk.Button(menu_frame, text="Головна").grid(row=0, column=0, padx=5)
tk.Button(menu_frame, text="Адвокати", bg="#cce6ff").grid(row=0, column=1, padx=5)
tk.Button(menu_frame, text="Клієнти").grid(row=0, column=2, padx=5)
tk.Button(menu_frame, text="Продажі").grid(row=0, column=3, padx=5)

# Форма введення
form_frame = tk.Frame(root)
form_frame.pack(pady=10)

tk.Label(form_frame, text="Ініціали адвоката").grid(row=0, column=0, padx=5, pady=5)
initials_entry = tk.Entry(form_frame)
initials_entry.grid(row=0, column=1, padx=5)

tk.Label(form_frame, text="Номер адвокатського свідоцтва").grid(row=1, column=0, padx=5, pady=5)
certificate_entry = tk.Entry(form_frame)
certificate_entry.grid(row=1, column=1, padx=5)

tk.Label(form_frame, text="Досвід").grid(row=2, column=0, padx=5, pady=5)
experience_entry = tk.Entry(form_frame)
experience_entry.grid(row=2, column=1, padx=5)

tk.Label(form_frame, text="Спеціалізація").grid(row=3, column=0, padx=5, pady=5)
specialization_entry = tk.Entry(form_frame)
specialization_entry.grid(row=3, column=1, padx=5)

tk.Button(root, text="Додати", command=add_lawyer, bg="#3399ff", fg="white").pack(pady=10)

# Таблиця адвокатів
columns = ("Ініціали", "Досвід", "Номер свідоцтва", "Спеціалізація")
lawyers_tree = ttk.Treeview(root, columns=columns, show="headings")
for col in columns:
    lawyers_tree.heading(col, text=col)
lawyers_tree.pack(pady=10, fill=tk.BOTH, expand=True)

# Запуск GUI
root.mainloop()
