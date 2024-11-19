# **Enemy AI ECS Showcase**  

![Unity](https://img.shields.io/badge/Unity-2021.3%2B-blue)  
![LeoECS](https://img.shields.io/badge/LeoECS-v1.0-green)  
![License](https://img.shields.io/badge/license-MIT-blue)  

---

## **Описание проекта**  
Этот проект — пет-проект, созданный для демонстрации работы с ECS (Entity-Component-System) архитектурой с использованием фреймворка [LeoECS](https://github.com/Leopotam/ecs.git) и интегратора [LeoECS Unity Integration](https://github.com/Leopotam/ecs-unityintegration.git).  

**Цель проекта** — продемонстрировать навыки проектирования и реализации игровой логики на основе ECS, а также опыт работы с Unity в рамках высокоэффективного подхода к управлению данными.  

Проект использует **Unity URP (Universal Render Pipeline)** для создания качественной визуализации, сохраняя при этом высокую производительность.

---

## **Функциональность**  
- **Искусственный интеллект врагов**:  
  - Враги имеют несколько состояний (Idle, Pursue, Chase, Attack, Walk), переключающихся на основе расстояния до игрока и других условий.  
  - Управление состояниями осуществляется с использованием ECS для максимальной производительности.  

- **Персонаж от 3-го лица**:  
  - В проекте реализован персонаж от третьего лица, которым можно управлять через стандартное управление с использованием клавиш и мыши.  
  - Камера следует за персонажем, создавая типичный опыт игры от третьего лица.

- **Взаимодействие с предметами**:  
  - Возможность взаимодействовать с различными предметами на сцене. Персонаж может подбирать объекты и использовать их в игровом процессе.

- **Инвентарь**:  
  - Реализована система инвентаря, в которой игрок может хранить различные предметы, такие как оружие и артефакты.  

- **Оружие**:  
  - В проекте присутствуют несколько типов оружия, которые можно использовать для атаки врагов, например, пистолет или нож.  
  - Каждое оружие имеет свои характеристики и может быть выбрано или оснащено игроком через инвентарь.

- **Использование LeoECS**:  
  - Реализация логики AI и инвентаря через энтити-компоненты и системы, обеспечивая четкое разделение данных и поведения.  
  - Упрощенная и модульная структура кода.  

---

## **Технологии**  
| Технология              | Версия                      |  
|--------------------------|-----------------------------|  
| Unity                   | 2021.3 или выше            |  
| Universal Render Pipeline (URP) | Последняя версия       |  
| LeoECS                  | [Репозиторий](https://github.com/Leopotam/ecs.git) |  
| LeoECS Unity Integration | [Репозиторий](https://github.com/Leopotam/ecs-unityintegration.git) |  

---

## **Демонстрация проекта**  
Этот проект демонстрирует:  
1. **Искусственный интеллект врагов**: Показаны различные состояния врагов и их переходы (например, преследование, погоня, атака).  
2. **Управление персонажем**: Реализована камера от третьего лица и базовая механика взаимодействия.  
3. **Система инвентаря**: Показано управление предметами, включая взаимодействие и использование оружия.  
4. **ECS-архитектура**: Продемонстрирована высокопроизводительная структура на базе LeoECS.  

---

## **Как запустить проект**  
1. Склонируйте репозиторий:  
   ```bash
   git clone https://github.com/rusoffline/LeoECS_Shooter.git

## **Контакты**
- [hh.ru резюме](https://ufa.hh.ru/resume/7c0eef23ff02be4b310039ed1f77645a586570)
- 📧 **Email**: example@mail.com  
- 📞 **Телефон**: +7 929 214 00 97  
- 💬 **WhatsApp**: +7 929 214 00 97  
- 📱 **Telegram**: [@rusoffline](https://t.me/rusoffline)  
