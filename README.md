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

- **Использование LeoECS**:  
  - Реализация логики AI через энтити-компоненты и системы, обеспечивая четкое разделение данных и поведения.  
  - Упрощенная и модульная структура кода.  

- **Интеграция с Unity**:  
  - Использование интегратора [ecs-unityintegration](https://github.com/Leopotam/ecs-unityintegration.git) для удобной работы с Unity-компонентами (например, анимацией, NavMesh и т.д.).  

---

## **Технологии**  
| Технология              | Версия                      |  
|--------------------------|-----------------------------|  
| Unity                   | 2021.3 или выше            |  
| Universal Render Pipeline (URP) | Последняя версия       |  
| LeoECS                  | [Репозиторий](https://github.com/Leopotam/ecs.git) |  
| LeoECS Unity Integration | [Репозиторий](https://github.com/Leopotam/ecs-unityintegration.git) |  

---

## **Целевая аудитория**  
Проект ориентирован на работодателей и разработчиков, интересующихся современными подходами к созданию игр.  
Данный пет-проект демонстрирует:  
1. Опыт работы с ECS-архитектурой.  
2. Понимание эффективного управления данными в игровых проектах.  
3. Навыки реализации AI в рамках высокопроизводительных систем.  

---

## **Как запустить проект**  
1. Склонируйте репозиторий:  
   ```bash
   git clone https://github.com/rusoffline/LeoECS_Shooter.git
