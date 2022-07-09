using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordRepositoryCacheFactory
{
    private WordRepositoryFactory wordRepositoryFactory;

    public WordRepositoryCacheFactory(WordRepositoryFactory wordRepositoryFactory) {
        this.wordRepositoryFactory = wordRepositoryFactory;
    }

    public WordRepository getWordRepository(int numberOfWords) {
        WordRepository wordRepository = wordRepositoryFactory.getWordRepository(numberOfWords);
        WordRepository cachedWordRepository = new CachedWordRepository(wordRepository);
        return cachedWordRepository;
    }
}
