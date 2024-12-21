class User:
    def __init__(self, user_id, name, email):
        self.user_id = user_id
        self.name = name
        self.email = email

    def __str__(self):
        return f"User({self.user_id}, {self.name}, {self.email})"

class UserFactory:
    @staticmethod
    def create_user(user_type, user_id, name, email):
        if user_type == "regular":
            return User(user_id, name, email)
        else:
            raise ValueError("Unsupported user type")

class RecipeDatabase:
    _instance = None

    def __new__(cls, *args, **kwargs):
        if not cls._instance:
            cls._instance = super(RecipeDatabase, cls).__new__(cls)
            cls._instance.recipes = []
        return cls._instance

    def add_recipe(self, recipe):
        self.recipes.append(recipe)

    def get_all_recipes(self):
        return self.recipes

class Recipe:
    def __init__(self, title, ingredients, instructions):
        self.title = title
        self.ingredients = ingredients
        self.instructions = instructions

    def get_details(self):
        return f"Recipe: {self.title}, Ingredients: {', '.join(self.ingredients)}"

class RecipeDecorator:
    def __init__(self, recipe):
        self.recipe = recipe

    def get_details(self):
        return self.recipe.get_details()

class FavoriteRecipeDecorator(RecipeDecorator):
    def get_details(self):
        return f"[FAVORITE] {super().get_details()}"

class SearchStrategy:
    def search(self, query, recipes):
        raise NotImplementedError

class SearchByTitle(SearchStrategy):
    def search(self, query, recipes):
        return [recipe for recipe in recipes if query.lower() in recipe.title.lower()]

class SearchByIngredient(SearchStrategy):
    def search(self, query, recipes):
        return [recipe for recipe in recipes if query.lower() in [i.lower() for i in recipe.ingredients]]

class RecipeSearchController:
    def __init__(self, strategy: SearchStrategy):
        self.strategy = strategy

    def set_strategy(self, strategy: SearchStrategy):
        self.strategy = strategy

    def search(self, query, recipes):
        return self.strategy.search(query, recipes)

class RecipeNotifier:
    def __init__(self):
        self.subscribers = []

    def subscribe(self, user):
        self.subscribers.append(user)

    def notify(self, recipe):
        for subscriber in self.subscribers:
            print(f"Notification to {subscriber.name}: New recipe added - {recipe.title}")

class UserInterface:
    def __init__(self, user, recipe_db, search_controller):
        self.user = user
        self.recipe_db = recipe_db
        self.search_controller = search_controller

    def display_recipes(self):
        for recipe in self.recipe_db.get_all_recipes():
            print(recipe.get_details())

    def search_recipes(self, query):
        results = self.search_controller.search(query, self.recipe_db.get_all_recipes())
        print(f"Search results for '{query}':")
        for result in results:
            print(result.get_details())

# Singleton: Database instance
recipe_db = RecipeDatabase()

# Добавление рецептов
recipe1 = Recipe("Spaghetti", ["pasta", "tomato sauce"], "Boil pasta and add sauce.")
recipe2 = Recipe("Salad", ["lettuce", "tomato", "cucumber"], "Mix all ingredients.")
recipe_db.add_recipe(recipe1)
recipe_db.add_recipe(recipe2)

# Decorator: Favorite recipe
favorite_recipe = FavoriteRecipeDecorator(recipe1)

# Factory: Создание пользователя
user = UserFactory.create_user("regular", 1, "John Doe", "john@example.com")

# Strategy: Поиск рецептов
search_controller = RecipeSearchController(SearchByTitle())
search_controller.set_strategy(SearchByIngredient())

# Observer: Уведомления
notifier = RecipeNotifier()
notifier.subscribe(user)
notifier.notify(recipe1)

# UserInterface: Взаимодействие
ui = UserInterface(user, recipe_db, search_controller)
ui.display_recipes()
ui.search_recipes("tomato")