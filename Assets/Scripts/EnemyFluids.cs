using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFluids : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> particleCollisionEvent;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleCollisionEvent = new List<ParticleCollisionEvent>();
    }

    //Считывает столкновение объектов с частицами
    private void OnParticleCollision(GameObject other)
    {
        //Содержит количество столкновений частицы и обьектов
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, particleCollisionEvent);

        //Цикл для нанесения урона
        for (int i = 0; i < numCollisionEvents; i++)
        {
            //Содержит объект с которым взаимодействуют частицы
            var collider = particleCollisionEvent[i].colliderComponent;
            //Если у объекта который столкнулся с частицей есть тэг "Enemy", то объект получает урон
            if (collider.CompareTag("Character"))
            {
                print('a');
            }
        }
    }
}
