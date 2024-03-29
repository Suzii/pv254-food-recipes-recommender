-- ===== statistics of user testing ===
-- all data
SELECT TOP (1000) *
FROM [dbo].[RecommendationUsed];

-- total clicks count
SELECT count(*) 
FROM [dbo].[RecommendationUsed] 
WHERE id > 1114;

-- clicks by recommendation used
SELECT RecommendedBy, count(*) as clicks 
FROM [dbo].[RecommendationUsed] 
WHERE id > 1114 
GROUP BY RecommendedBy 
ORDER BY clicks DESC;

-- AB test data for each recipe
SELECT	COALESCE(TfIdfData.DisplayedRecipeId, IngredientBasedData.DisplayedRecipeId) AS RecipeId, 
		COALESCE(TfIdf, 0) AS 'TF-IDF Clicks', 
		COALESCE(IngredientBased, 0) AS 'Ingredient Based Clicks'
FROM 
	(SELECT DisplayedRecipeId, count(*) as TfIdf 
	FROM [dbo].[RecommendationUsed] 
	WHERE id > 1114 AND RecommendedBy = 'TfIdf'
	GROUP BY DisplayedRecipeId) AS TfIdfData
FULL OUTER JOIN 
	(SELECT DisplayedRecipeId, count(*) as IngredientBased 
	FROM [dbo].[RecommendationUsed] 
	WHERE id > 1114 AND RecommendedBy = 'IngredientBased'
	GROUP BY DisplayedRecipeId) AS IngredientBasedData
ON (TfIdfData.DisplayedRecipeId = IngredientBasedData.DisplayedRecipeId)
ORDER BY RecipeId ASC